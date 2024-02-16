using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;

public class SessionCreator : MonoBehaviour
{
    private FarmSystem farmSystem;
    private PlayerSystem skinSystem;
    private DojoSystem dojoSystem;
    private UpdaterSystem updaterSystem;

    private void Awake()
    {
        farmSystem = UnityUtils.FindOrCreateComponent<FarmSystem>();
        skinSystem = UnityUtils.FindOrCreateComponent<PlayerSystem>();
        dojoSystem = UnityUtils.FindOrCreateComponent<DojoSystem>();
        updaterSystem = UnityUtils.FindOrCreateComponent<UpdaterSystem>();
    }

    // public async Task<Session> Create(string username, string password)
    // {
    //     PlayerFarmState playerFarmState = await FindOrCreatePlayer(username, password);
        
    //     Session session = UnityUtils.FindOrCreateComponent<Session>();
    //     DontDestroyOnLoad(session.gameObject);
        
    //     session.PlayerId = playerFarmState.player_id;
    //     session.MapId = playerFarmState.map_id;
    //     session.FarmId = playerFarmState.farm_id;   

    //     return session;
    // }

    public Session GetSessionFromExistingPlayer(string username, string password)
    {
        PlayerFarmState playerFarmState = FindPlayerFarmState(username, password);
        
        if(playerFarmState != null)
        {
            Session session = UnityUtils.FindOrCreateComponent<Session>();
            DontDestroyOnLoad(session.gameObject);
            
            session.PlayerId = playerFarmState.player_id;
            session.MapId = playerFarmState.map_id;
            session.FarmId = playerFarmState.farm_id;   

            return session;
        }

        return null;
    }
    
    private PlayerFarmState FindPlayerFarmState(string username, string password)
    {
        string playerId = GetPlayerHash(username, password);
        PlayerFarmState playerFarmState = FindPlayer(playerId);
        
        if (playerFarmState != null)
            return playerFarmState;

        return null;
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

    public void CreateNewPlayer(string username, string password, SkinType gender)
    {
        PlayerFarmState playerFarmState = FindPlayerFarmState(username, password);
        if (playerFarmState == null)
        {
            string playerId = GetPlayerHash(username, password);
            string usernameHex = StringToHex(username);

            dojo.Call skinCall = skinSystem.CreatePlayer(playerId, usernameHex, gender, dojoSystem.Systems.playerSystemAdress);
            dojo.Call farmCall = farmSystem.CreateFarm(playerId, dojoSystem.Systems.farmSystemAdress);
            dojo.Call updaterCall = updaterSystem.Connect(playerId, dojoSystem.Systems.updaterSystemAddress);
            
            try
            {
                dojoSystem.ExecuteCalls(new[] { skinCall, farmCall});
            }
            catch (Exception e) 
            {
                Debug.LogException(e);
            }
        }
        else
        {
            throw new Exception("The playe exist!!");
        }
    }


    private string StringToHex(string input)
    {
        // Convert the string to a byte array
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        
        // Convert each byte to its hexadecimal representation
        StringBuilder hexBuilder = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            hexBuilder.AppendFormat("{0:x2}", b); // "x2" formats the byte as a two-digit hexadecimal number
        }
        
        // Return the hexadecimal string
        return hexBuilder.ToString();
    }
}
