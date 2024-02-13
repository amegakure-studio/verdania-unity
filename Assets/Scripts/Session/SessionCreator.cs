using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SessionCreator : MonoBehaviour
{
    private FarmSystem farmSystem;
    private SkinSystem skinSystem;

    private void Awake()
    {
        farmSystem = UnityUtils.FindOrCreateComponent<FarmSystem>();
        skinSystem = UnityUtils.FindOrCreateComponent<SkinSystem>();
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

    public async void CreateNewPlayer(string username, string password, SkinType gender)
    {
        PlayerFarmState playerFarmState = FindPlayerFarmState(username, password);
        if (playerFarmState == null)
        {
            string playerId = GetPlayerHash(username, password);
            
            try
            {
                string usernameHex = StringToHex(username);
                await skinSystem.CreatePlayer(playerId, usernameHex, gender);
            }
            catch (Exception e)
            {
                Debug.LogError("Error in skin system: " + e );
                throw new Exception("Error in skin system");
            }

            await Task.Delay(1000);

            try
            {
                await farmSystem.CreateFarm(playerId);
            }
            catch (Exception e)
            {
                Debug.LogError("Error in farm system: " + e );
                throw new Exception("Error in farm system");
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
