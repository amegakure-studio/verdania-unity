using Amegakure.Verdania.DojoModels;
using Amegakure.Verdania.GridSystem;
using Dojo;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MapRenderer : MonoBehaviour
{  
    [SerializeField] FolderResourcesConfig folderResourcesConfig;

    private WorldManager m_WorldManager;
    private Session m_Session;
    private MapFinder m_Finder;
    private Dictionary<Vector2Int, TileRenderer> m_TileRenderers;

    private void OnEnable()
    {
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();
        m_WorldManager.OnEntityFeched += Render;
    }

    void Awake()
    {
        m_TileRenderers = new Dictionary<Vector2Int, TileRenderer>();
        GameObject.FindObjectsOfType<TileRenderer>()
            .ToList()
            .ForEach(tileRenderer => m_TileRenderers.Add(tileRenderer.coordinate, tileRenderer));

        m_Session = GameObject.FindObjectOfType<Session>();

        m_Finder = GameObject.FindObjectOfType<MapFinder>();
    }

    void OnDisable()
    {
        m_WorldManager.OnEntityFeched -= Render;
    }

    private void Render(WorldManager worldManager)
    {
        RenderMap(worldManager);
        RenderObject(worldManager);
    }

    private void RenderMap(WorldManager worldManager)
    {
        List<Tile> tiles = m_Finder.GetTilesByMapID(m_Session.MapId, worldManager.Entities());
        
        foreach (Tile tile in tiles) 
        {
            Vector2Int tileCoordinate = new((int)tile.x, (int)tile.y);

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

    private void RenderObject(WorldManager worldManager)
    {
        GameObject[] entities = worldManager.Entities();
        List<TileState> tilesState = m_Finder.GetTileStatesByFarmID(m_Session.FarmId, entities);
        
        TileState tileState = tilesState[0];
        if ((TileStateT) tileState.entityType == TileStateT.Crop)
        {
            CropState cropState = m_Finder.GetCropStateByIndex(tileState.farmId, tileState.entityIndex, entities);
            
            if (cropState != null)
            {
                Crop crop = m_Finder.GetCropById(cropState.cropId, entities);
                Debug.Log(crop.name);
            }
        }

        if ((TileStateT)tileState.entityType == TileStateT.Enviroment)
        {
            EnvEntityState envEntityState = m_Finder.GetEnvEntityStateByIndex(tileState.farmId, tileState.entityIndex, entities);

            if (envEntityState != null)
            {
                Debug.Log("index: " + envEntityState.index);
                Debug.Log("envEntityid: " + envEntityState.envEntityId);
                Debug.Log("x: " + envEntityState.x + "y: " + envEntityState.y);
                EnvEntity envEntity = m_Finder.GetEnvEntityById(envEntityState.envEntityId, entities);
                Debug.Log(envEntity.name);
            }
        }
    }
}
