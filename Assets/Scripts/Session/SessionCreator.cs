using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System.Linq;
using UnityEngine;

public class SessionCreator : MonoBehaviour
{
    public Session Create(string username, string password)
    {
        PlayerFarmState playerFarmState = FindOrCreatePlayer(username, password);
        
        Session session = UnityUtils.FindOrCreateComponent<Session>();
        DontDestroyOnLoad(session.gameObject);
        
        session.PlayerId = playerFarmState.player_id;
        session.MapId = playerFarmState.map_id;
        session.FarmId = playerFarmState.farm_id;   

        return session;
    }

    private PlayerFarmState FindOrCreatePlayer(string username, string password)
    {
        string playerId = GetPlayerHash(username, password);
        PlayerFarmState playerFarmState = FindPlayer(playerId);

        if (playerFarmState == null)
        {
            CreatePlayer(playerId);
            playerFarmState = FindPlayer(playerId);
        }

        return playerFarmState;
    }

    private PlayerFarmState FindPlayer(string playerId)
    {
        
        WorldManager worldManager = GameObject.FindAnyObjectByType<WorldManager>();

        PlayerFarmState playerFarmState = worldManager.Entities()
                .ToList()
                .Select(entity => entity.GetComponent<PlayerFarmState>())
                .FirstOrDefault(farmStateComponent => farmStateComponent != null && farmStateComponent.player_id.Equals(playerId));

        return playerFarmState;
    }

    private void CreatePlayer(string playerId)
    {
        var player_id = new FieldElement(playerId).Inner();
        DojoCaller dojoCaller = UnityUtils.FindOrCreateComponent<DojoCaller>();

        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id
            },
            to = dojoCaller.GetSystems().farmSystemAdress,
            selector = "create_farm"
        };
        
        dojoCaller.ExecuteCall(call);
    }

    private string GetPlayerHash(string username, string password)
    {
        var playerHash = new Hash128();
        playerHash.Append(username);
        playerHash.Append(password);

        return playerHash.ToString();
    }
}
