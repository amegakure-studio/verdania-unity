using System;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PathFinder pathFinder;
    private Character character;

    public Character Character { get => character; set => character = value; }

    private void Awake()
    {
        pathFinder = GameObject.FindObjectOfType<PathFinder>();
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

            if (Input.GetKeyDown(KeyCode.K))
                EventManager.Instance.Publish(GameEvent.CHARACTER_HOE, new() { { "Character", Character.gameObject } });

            else if (Input.GetKeyDown(KeyCode.L))
                EventManager.Instance.Publish(GameEvent.CHARACTER_PLANT, new() { { "Character", Character.gameObject } });

            else if (Input.GetKeyDown(KeyCode.J))
                EventManager.Instance.Publish(GameEvent.CHARACTER_WATER, new() { { "Character", Character.gameObject } });
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
                SetTargetPosition(tile);
            }
        }
    }

    private void SetTargetPosition(TileRenderer targetTile)
    {
        if(targetTile.coordinate != Character.CurrentTile.coordinate)
        {
            Character.CurrentPathIndex = 0;
            List<TileRenderer> tiles = new();
            tiles.AddRange(pathFinder.FindPath(Character.CurrentTile, targetTile).tiles);
            
            Character.PathVectorList = tiles;
        }
    }
}
