using Amegakure.Verdania.DojoModels;
using Amegakure.Verdania.GridSystem;
using Dojo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{  
    [SerializeField] FolderResourcesConfig folderResourcesConfig;

    private WorldManager m_WorldManager;
    private Session m_Session;
    private MapFinder m_Finder;
    private Dictionary<Vector2Int, TileRenderer> m_TileRenderers;
    private Dictionary<Vector2Int, Tile> mapTiles;

    [Header("Tiles")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material highlightMaterial;

    private void OnEnable()
    {
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();
        m_WorldManager.OnEntityFeched += Render;

        EventManager.Instance.Subscribe(GameEvent.SPAWN_ENVELEMENT, HandleSpawnEnvElement);
        EventManager.Instance.Subscribe(GameEvent.SPAWN_CROP, HandleSpawnCrop);
        EventManager.Instance.Subscribe(GameEvent.SPAWN_TILESTATE, HandleTileStateSpawn);
    }

    private void HandleTileStateSpawn(Dictionary<string, object> obj)
    {
        try
        {
            TileState tileState = (TileState)obj["Element"];

            tileState.TileStateChanged += OnTileStateChanged;
            //RenderObject(m_WorldManager.Entities(), tileState);
            var entities = m_WorldManager.Entities();

            if ((TileStateT)tileState.entityType == TileStateT.Crop)
            {

                CropState cropState = m_Finder.GetCropStateByIndex(tileState.farmId, tileState.entityIndex, entities);
                cropState.cropChanged += OnCropChanged;
                createCrops(entities, cropState);
            }

            if ((TileStateT)tileState.entityType == TileStateT.Enviroment)
            {
                EnvEntityState envEntityState = m_Finder.GetEnvEntityStateByIndex(tileState.farmId, tileState.entityIndex, entities);
                createEnvEntities(entities, envEntityState);
            }


        }
        catch (Exception e) { }
        
    }

    private void HandleSpawnEnvElement(Dictionary<string, object> context)
    {
        try
        {
            EnvEntityState envState = (EnvEntityState)context["Element"];
            Debug.Log("Env element SPAWNED");
            createEnvEntities(m_WorldManager.Entities(), envState);

        } catch { }
    }

    private void HandleSpawnCrop(Dictionary<string, object> context)
    {
        try
        {
            //CropState cropState = (CropState)context["Element"];
            //cropState.cropChanged += OnCropChanged;

            //createCrops(m_WorldManager.Entities(), cropState);
        }
        catch { }
    }

    private void OnCropChanged(CropState cropState)
    {
        Debug.Log("CROP CHANGED EVENT RECEIVED!");
        createCrops(m_WorldManager.Entities(), cropState);
    }

    void Awake()
    {
        m_TileRenderers = new Dictionary<Vector2Int, TileRenderer>();
        GameObject.FindObjectsOfType<TileRenderer>()
            .ToList()
            .ForEach(tileRenderer => m_TileRenderers.Add(tileRenderer.coordinate, tileRenderer));

        m_Session = GameObject.FindObjectOfType<Session>();

        m_Finder = GameObject.FindObjectOfType<MapFinder>();
        mapTiles = new();
    }

    void OnDisable()
    {
        m_WorldManager.OnEntityFeched -= Render;
        EventManager.Instance.Unsubscribe(GameEvent.SPAWN_ENVELEMENT, HandleSpawnEnvElement);
        EventManager.Instance.Unsubscribe(GameEvent.SPAWN_CROP, HandleSpawnCrop);
    }

    public TileRenderer GetTileRenderer(Vector2Int coordinate)
    {
        if (m_TileRenderers.ContainsKey(coordinate))
            return m_TileRenderers[coordinate];

        else return null;
    }

    public Tile GetMapTile(Vector2Int coordinate)
    {
        if (mapTiles.ContainsKey(coordinate))
            return mapTiles[coordinate];

        else return null;
    }

    public void HighlightTile(TileRenderer tile)
    {
        tile.GetComponentInChildren<Renderer>().material = highlightMaterial;
    }

    public void UnHighlightTile(TileRenderer tile)
    {
        tile.GetComponentInChildren<Renderer>().material = defaultMaterial;
    }

    private void Render(WorldManager worldManager)
    {
        Debug.Log("Called?");
        RenderMap(worldManager);
        RenderObjects(worldManager);
        RenderPlayer(worldManager);
    }

    private void RenderPlayer(WorldManager worldManager)
    {
        PlayerState playerState = m_Finder.GetPlayerStateById(m_Session.PlayerId.Hex(), m_Session.FarmId, worldManager.Entities());
        
        if (playerState != null)
        {
            PlayerSkin playerSkin = m_Finder.GetPlayerSkinById(m_Session.PlayerId.Hex(), worldManager.Entities());
            Vector2Int tileCoordinate = new((int)playerState.x, (int)playerState.y);

            if (m_TileRenderers.ContainsKey(tileCoordinate))
            {
                TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];
                string characterPath = folderResourcesConfig.charactersFolder + playerSkin.gender.ToString() + "Vrm";
                
                GameObject characterPrefab = Resources.Load<GameObject>(characterPath);
                GameObject characterGo = Instantiate(characterPrefab);
                Debug.Log("Creation");

                characterGo.transform.position = tileRenderer.transform.position;

                Character character = characterGo.GetComponent<Character>();
                if (character != null) 
                {
                    Debug.Log("set coordinate");
                    character.CurrentTile = tileRenderer;
                    character.DojoId = m_Session.PlayerId.Hex();
                }

                PlayerController controller = characterGo.AddComponent<PlayerController>();
                controller.Character = character;
                controller.PlayerState = playerState;

                EventManager.Instance.Publish(GameEvent.PLAYER_CREATED, new() { { "Player", character } });
            }
        }

    }

    private void RenderMap(WorldManager worldManager)
    {
        List<Tile> tiles = m_Finder.GetTilesByMapID(m_Session.MapId, worldManager.Entities());
        
        foreach (Tile tile in tiles) 
        {
            Vector2Int tileCoordinate = new((int)tile.x, (int)tile.y);

            mapTiles[tileCoordinate] = tile;

            if (m_TileRenderers.ContainsKey(tileCoordinate))
            {
                if (tile.tileType == TileType.Bridge || tile.tileType == TileType.Building)
                {
                    TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];
                    GameObject objectPrefab = Resources.Load<GameObject>(folderResourcesConfig.mapFolder + tile.tileType.ToString());

                    tileRenderer.OccupyingObject = objectPrefab;
                }
                if (tile.tileType == TileType.Water || tile.tileType == TileType.NotWalkable)
                {
                    TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];
                    tileRenderer.Occupied = true;
                }
            }
            else
                Debug.LogError("Tile " + tileCoordinate.ToString() + " exists in dojo but not in Unity");

        }
    }

    private void OnTileStateChanged(TileState tileState)
    {
        RenderObject(m_WorldManager.Entities(), tileState);
    }

    private void RenderObjects(WorldManager worldManager)
    {
        GameObject[] entities = worldManager.Entities();
        List<TileState> tileStates = m_Finder.GetTileStatesByFarmID(m_Session.FarmId, entities);
        
        foreach(TileState tileState in tileStates)
        {
            tileState.TileStateChanged += OnTileStateChanged;
            RenderObject(entities, tileState);
        }

        //DebugCropCreator(new(0, 38), "Corn", "0");
        //DebugCropCreator(new(0, 39), "Corn", "25");
        //DebugCropCreator(new(0, 40), "Corn", "50");
        //DebugCropCreator(new(0, 41), "Corn", "100");


        //DebugCropCreator(new(1, 38), "Mushroom", "0");
        //DebugCropCreator(new(1, 39), "Mushroom", "25");
        //DebugCropCreator(new(1, 40), "Mushroom", "50");
        //DebugCropCreator(new(1, 41), "Mushroom", "100");


        //DebugCropCreator(new(2, 38), "Onion", "0");
        //DebugCropCreator(new(2, 39), "Onion", "25");
        //DebugCropCreator(new(2, 40), "Onion", "50");
        //DebugCropCreator(new(2, 41), "Onion", "100");


        //DebugCropCreator(new(3, 38), "Carrot", "0");
        //DebugCropCreator(new(3, 39), "Carrot", "25");
        //DebugCropCreator(new(3, 40), "Carrot", "50");
        //DebugCropCreator(new(3, 41), "Carrot", "100");


        //DebugCropCreator(new(4, 38), "Pumpkin", "0");
        //DebugCropCreator(new(4, 39), "Pumpkin", "25");
        //DebugCropCreator(new(4, 40), "Pumpkin", "50");
        //DebugCropCreator(new(4, 41), "Pumpkin", "100");
    }

    private void RenderObject(GameObject[] entities, TileState tileState)
    {
        if ((TileStateT)tileState.entityType == TileStateT.Crop)
        {
            CropState cropState = m_Finder.GetCropStateByIndex(tileState.farmId, tileState.entityIndex, entities);
            createCrops(entities, cropState);
        }

        if ((TileStateT)tileState.entityType == TileStateT.Enviroment)
        {
            EnvEntityState envEntityState = m_Finder.GetEnvEntityStateByIndex(tileState.farmId, tileState.entityIndex, entities);
            createEnvEntities(entities, envEntityState);
        }
    }

    private void DebugCropCreator(Vector2Int tileCoordinate, string cropName, string cropGrowId )
    {
        
        TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];

        string landPath = folderResourcesConfig.objectsFolder + "SuitableForCrop";
        GameObject landPrefab = Resources.Load<GameObject>(landPath);
        tileRenderer.OccupyingObject = landPrefab;

        string cropPath = folderResourcesConfig.cropsFolder + cropName + "/" + cropName + "_" + cropGrowId;

        GameObject cropPrefab = Resources.Load<GameObject>(cropPath);
        tileRenderer.AddObject(cropPrefab);
    }

    private void createEnvEntities(GameObject[] entities, EnvEntityState envEntityState)
    {

        if (envEntityState != null)
        {
            Debug.Log("index: " + envEntityState.index);
            Debug.Log("envEntityid: " + envEntityState.envEntityId);
            Debug.Log("x: " + envEntityState.x + "y: " + envEntityState.y);

            Vector2Int tileCoordinate = new((int)envEntityState.x, (int)envEntityState.y);

            if (m_TileRenderers.ContainsKey(tileCoordinate))
            {
                EnvEntity envEntity = m_Finder.GetEnvEntityById(envEntityState.envEntityId, entities);

                if (envEntity != null)
                {
                    TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];

                    string nameString = HexToString(envEntity.entityName.Hex());

                    Debug.Log("HEX: " + envEntity.entityName.Hex() + " Name: " + nameString + ". dojo coord: " + tileCoordinate +
                        ". tile renderer coord: " + tileRenderer.coordinate);

                    GameObject objectPrefab = Resources.Load<GameObject>(folderResourcesConfig.objectsFolder + nameString.Replace(" ", ""));                           
                    tileRenderer.OccupyingObject = objectPrefab;

                    EnvEntityT entityType = (EnvEntityT)envEntity.id;

                    if (entityType != EnvEntityT.SuitableForCrop || entityType != EnvEntityT.Rock || entityType != EnvEntityT.Grass)
                        tileRenderer.Occupied = true;
                    else
                        tileRenderer.Occupied = false;
                }
            }
        }
    }

    private string HexToString(string hexString)
    {
        // Remove the "0x" prefix
        hexString = hexString.Substring(2);
        
        // Find the index of the first non-zero character
        int firstNonZeroIndex = 0;
        for (int i = 0; i < hexString.Length; i++)
        {
            if (hexString[i] != '0')
            {
                firstNonZeroIndex = i;
                break;
            }
        }

        // Extract the substring from the first non-zero character to the end of the string
        hexString = hexString.Substring(firstNonZeroIndex);

        // Convert the hex string to bytes
        byte[] bytes = new byte[hexString.Length / 2];

        for (int i = 0; i < hexString.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        // Trim trailing null bytes
        int length = Array.FindLastIndex(bytes, b => b != 0) + 1;
        byte[] trimmedBytes = new byte[length];
        Array.Copy(bytes, trimmedBytes, length);

        // Convert byte array to string using UTF-8 encoding
        string stringValue = Encoding.UTF8.GetString(trimmedBytes);

        return stringValue;
    }

    private void createCrops(GameObject[] entities, CropState cropState)
    {       
        if (cropState != null)
        {
            Debug.Log("Pos: " + cropState.x + cropState.y);
            Vector2Int tileCoordinate = new((int)cropState.x, (int)cropState.y);

            if (m_TileRenderers.ContainsKey(tileCoordinate))
            {
                
                Crop crop = m_Finder.GetCropById(cropState.cropId, entities);

                if (crop != null)
                {
                    TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];

                    if (cropState.harvested == true)
                        tileRenderer.OccupyingObject = null;

                    else
                    {
                        string growId = "";

                        if (cropState.growingProgress < 25)
                            growId = "0";
                        else if (cropState.growingProgress >= 25 && cropState.growingProgress < 50)
                            growId = "25";
                        else if (cropState.growingProgress >= 50 && cropState.growingProgress < 100)
                            growId = "50";
                        else if (cropState.growingProgress == 100)
                            growId = "100";

                        string prefabName = cropState.watered == true ? "Suitableforcrop_watered" : "Suitableforcrop";
                        string landPath = folderResourcesConfig.objectsFolder + prefabName;

                        GameObject landPrefab = Resources.Load<GameObject>(landPath);
                        tileRenderer.OccupyingObject = landPrefab;

                        string cropNameString = HexToString(crop.cropName.Hex());
                        Debug.Log("Cropname: " + cropNameString);

                        string cropPath = folderResourcesConfig.cropsFolder + cropNameString + "/" + cropNameString + "_" + growId;
                        GameObject cropPrefab = Resources.Load<GameObject>(cropPath);
                        tileRenderer.AddObject(cropPrefab);
                    }            
                }
            }
        }
    }
}
