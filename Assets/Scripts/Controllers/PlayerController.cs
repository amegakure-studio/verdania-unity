using System;
using System.Collections.Generic;
using System.Linq;
using Amegakure.Verdania.DojoModels;
using Amegakure.Verdania.GridSystem;
using Dojo;
using dojo_bindings;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PathFinder pathFinder;
    private Character character;
    private MapRenderer map;
    private WorldManager worldManager;
    private PlayerSystem playerSystem;
    private InteractSystem interactSystem;
    private DojoSystem dojoSystem;
    private InventoryItems inventoryItems;
    private Inventory inventory;
    private PlayerState playerState;

    private List<TileRenderer> highlightedInteractTiles;

    public Character Character { get => character; set => character = value; }
    public PlayerState PlayerState { get => playerState; set
            { 
                if(value != null)
                {
                    playerState = value;
                    playerState.positionChanged += OnPlayerPositionChanged;
                }
            }
    }

    private void OnEnable()
    {
        worldManager = GameObject.FindObjectOfType<WorldManager>();
        //EventManager.Instance.Subscribe(GameEvent.CHARACTER_MOVE_END, HandleCharacterMoveEnd);
    }

    private void OnDisable()
    {
        //EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_MOVE_END, HandleCharacterMoveEnd);
    }

    //private void HandleCharacterMoveEnd(Dictionary<string, object> obj)
    //{
    //    try
    //    {
    //        Character character = (Character)obj["Character"];
    //        SelectAdjacentTiles();

    //    } catch(Exception ex) { }
    //}

    private void Start()
    {
        SelectAdjacentTiles();
    }

    private void Awake()
    {
        highlightedInteractTiles = new();
        pathFinder = GameObject.FindObjectOfType<PathFinder>();
        map = GameObject.FindObjectOfType<MapRenderer>();
        playerSystem = UnityUtils.FindOrCreateComponent<PlayerSystem>();
        interactSystem = UnityUtils.FindOrCreateComponent<InteractSystem>();
        dojoSystem = UnityUtils.FindOrCreateComponent<DojoSystem>();
        inventoryItems = Resources.Load<InventoryItems>("InventoryItems");
        inventory = GameObject.FindObjectOfType<Inventory>();     
    }

    private void OnDestroy()
    {
        playerState.positionChanged -= OnPlayerPositionChanged;
    }

    private void OnPlayerPositionChanged(PlayerState obj)
    {
        TileRenderer targetTile = map.GetTileRenderer(new Vector2Int((int)obj.x, (int)obj.y));

        Character.CurrentPathIndex = 0;
        List<TileRenderer> tiles = new();

        var path = pathFinder.FindPath(Character.CurrentTile, targetTile);

        if (path != null)
        {
            tiles.AddRange(path.tiles);
            Character.PathVectorList = tiles;
            SelectAdjacentTiles();
        }
    }

    private void SelectAdjacentTiles()
    {
        if (highlightedInteractTiles.Count != 0)
            highlightedInteractTiles.ForEach(tile => map.UnHighlightTile(tile));

        List<TileRenderer> adjacentTiles = GetPlayerAdjacentTiles();

        adjacentTiles.ForEach(tile => map.HighlightTile(tile));
        highlightedInteractTiles = adjacentTiles;
    }

    void Update()
    {
        if(Character != null)
        {
            Character.HandleMovement();

            string clickType = Input.GetMouseButtonDown(0) ? "Left" :
                        Input.GetMouseButtonDown(1) ? "Right" : null;

            if (clickType != null)
                HandleClick(clickType);
        }
    }

    private void HandleClick(string clickType)
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        int layerMask = 1 << LayerMask.NameToLayer("Tile");
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            TileRenderer tile = hit.collider.GetComponent<TileRenderer>();
            if (tile != null)
            {
                if (clickType == "Left")
                    SetTargetPosition(tile);
                else
                    Interact(tile);
            }
        }
    }

    private void Interact(TileRenderer targetTile)
    {
        if (GetPlayerAdjacentTiles().Contains(targetTile))
        {
            Tile mapTile = map.GetMapTile(targetTile.coordinate);
            
            if (mapTile != null) 
            {
                dojo.Call interactCall = interactSystem.Interact(character.DojoId, mapTile.id, dojoSystem.Systems.interactSystemAdress);

                try
                {                
                    dojoSystem.ExecuteCalls(new[] { interactCall });

                    foreach (InventoryItems.ItemMap item in inventoryItems.items)
                    {
                        if (item.itemType == inventory.GetEquippedItem())
                            EventManager.Instance.Publish(item.interactEvent, new() { { "Character", Character.gameObject } });
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw new Exception("Couldn't interact");
                }
            }
        }
    }

    private void SetTargetPosition(TileRenderer targetTile)
    {
        if(targetTile.coordinate != Character.CurrentTile.coordinate)
        {
            Tile mapTile = map.GetMapTile(targetTile.coordinate);

            if (mapTile != null)
            {
                dojo.Call moveCall = playerSystem.Move(character.DojoId, mapTile.x, mapTile.y, dojoSystem.Systems.playerSystemAdress);

                try
                {
                    dojoSystem.ExecuteCalls(new[] { moveCall });
                    Debug.Log("Character moved to: " + targetTile.coordinate);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw new Exception("Couldn't move");
                }
            }

        }
    }

    private List<TileRenderer> GetPlayerAdjacentTiles()
    {
        List<TileRenderer> tiles = new();

        Vector2Int originCoordinate = new Vector2Int((int)PlayerState.x, (int)PlayerState.y);
        List<Vector2Int> directions = new() 
        { 
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0)
        };

        foreach (Vector2Int direction in directions) 
        { 
            Vector2Int adjacentCoordinate = originCoordinate + direction;
            TileRenderer adjacentTile = map.GetTileRenderer(adjacentCoordinate);

            if (adjacentTile)
                tiles.Add(adjacentTile);
        }

        tiles.Add(map.GetTileRenderer(originCoordinate));

        return tiles;
    }
}
