using System;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PathFinder pathFinder;
    [SerializeField] Character character;

    void Update()
    {
        character.HandleMovement();

        string clickType = Input.GetMouseButtonDown(0) ? "Left" :
                    Input.GetMouseButtonDown(1) ? "Right" : null;

        if (clickType != null)
            HandleClick(clickType);
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
                Debug.Log(clickType + ": " + tile.coordinate.ToString());
                SetTargetPosition(tile);
            }
        }
    }

    private void SetTargetPosition(TileRenderer targetTile)
    {
        if(targetTile.coordinate != character.CurrentTile.coordinate)
        {
            character.CurrentPathIndex = 0;
            List<TileRenderer> tiles = new();
            tiles.AddRange(pathFinder.FindPath(character.CurrentTile, targetTile).tiles);
            
            character.PathVectorList = tiles;
        }
    }
}
