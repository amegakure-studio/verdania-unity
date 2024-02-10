using Dojo;
using System.Linq;
using UnityEngine;

public class SessionCreator : MonoBehaviour
{
    public Session Create(string username, string password)
    {
        PlayerFarmState playerFarmState = FindPlayer(username, password);
        
        Session session = UnityUtils.FindOrCreateComponent<Session>();
        DontDestroyOnLoad(session.gameObject);
        
        session.PlayerId = playerFarmState.player_id;
        session.MapId = playerFarmState.map_id;
        session.FarmId = playerFarmState.farm_id;   

        return session;
    }

    private PlayerFarmState FindPlayer(string username, string password)
    {
        string playerHash = GetPlayerHash(username, password);
        WorldManager worldManager = GameObject.FindAnyObjectByType<WorldManager>();

        PlayerFarmState playerFarmState = worldManager.Entities()
                .ToList()
                .Select(entity => entity.GetComponent<PlayerFarmState>())
                .FirstOrDefault(farmStateComponent => farmStateComponent != null && farmStateComponent.player_id.Equals(playerHash));

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
