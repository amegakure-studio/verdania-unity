using Amegakure.Verdania.DojoModels;
using Amegakure.Verdania.GridSystem;
using Dojo;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
}
