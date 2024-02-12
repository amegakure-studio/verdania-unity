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
        m_WorldManager.OnEntityFeched += Create;
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
        m_WorldManager.OnEntityFeched -= Create;
    }

    private void Create(WorldManager worldManager)
    {
        List<Tile> tiles = m_Finder.GetTilesByMapID(m_Session.MapId, worldManager.Entities());
        
        foreach (Tile tile in tiles) 
        { 
            if (tile.tileType == TileType.Bridge || tile.tileType == TileType.Building)
            {
                Vector2Int tileCoordinate = new((int)tile.x, (int)tile.y);
                 
                if (m_TileRenderers.ContainsKey(tileCoordinate))
                {       
                    TileRenderer tileRenderer = m_TileRenderers[tileCoordinate];
                    GameObject objectPrefab = Resources.Load<GameObject>(folderResourcesConfig.objectsFolder + tile.tileType.ToString());

                    tileRenderer.OccupyingObject = objectPrefab;
                }
                else
                    Debug.LogError("Tile " + tileCoordinate.ToString() + " exists in dojo but not in Unity");
            }              
        }
    }
}
