using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SessionCreator : MonoBehaviour
{
    private FarmSystem farmSystem;

    private void Awake()
    {
        farmSystem = UnityUtils.FindOrCreateComponent<FarmSystem>();
    }

    public async Task<Session> Create(string username, string password)
    {
        PlayerFarmState playerFarmState = await FindOrCreatePlayer(username, password);
        
        Session session = UnityUtils.FindOrCreateComponent<Session>();
        DontDestroyOnLoad(session.gameObject);
        
        session.PlayerId = playerFarmState.player_id;
        session.MapId = playerFarmState.map_id;
        session.FarmId = playerFarmState.farm_id;   

        return session;
    }

    private async Task<PlayerFarmState> FindOrCreatePlayer(string username, string password)
    {
        string playerId = GetPlayerHash(username, password);
        PlayerFarmState playerFarmState = FindPlayer(playerId);

        if (playerFarmState == null)
        {
            farmSystem.CreatePlayer(playerId);
            
            while (playerFarmState == null)
            {
                playerFarmState = FindPlayer(playerId);
                await Task.Delay(1000);
            }
        }
        return playerFarmState;
    }


    private PlayerFarmState FindPlayer(string playerId)
    {
        WorldManager worldManager = GameObject.FindAnyObjectByType<WorldManager>();
        string startValue = "0x00000000000000000000000000000000";

        PlayerFarmState playerFarmState = worldManager.Entities()
                .ToList()
                .Select(entity => entity.GetComponent<PlayerFarmState>())
                .FirstOrDefault(farmStateComponent => farmStateComponent != null && farmStateComponent.player_id.Hex().Equals(startValue + playerId));

        return playerFarmState;
    }

    private string GetPlayerHash(string username, string password)
    {
        var playerHash = new Hash128();
        playerHash.Append(username);
        playerHash.Append(password);

        return playerHash.ToString();
    }
}
