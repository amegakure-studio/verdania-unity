using Dojo;
using System.Linq;
using UnityEngine;

public class SessionCreator : MonoBehaviour
{
    public Session Create()
    {
        WorldManager worldManager = GameObject.FindAnyObjectByType<WorldManager>();
        
        PlayerFarmState playerFarmState = worldManager.Entities()
                .ToList()
                .Find(entity => entity.GetComponent<PlayerFarmState>() != null)
                .GetComponent<PlayerFarmState>();

        GameObject sessionGo = new("Session");
        Session session = sessionGo.AddComponent<Session>();
        session.PlayerId = playerFarmState.player_id;
        session.MapId = playerFarmState.map_id;
        session.FarmId = playerFarmState.farm_id;
        DontDestroyOnLoad(sessionGo);

        return session;
    }
}
