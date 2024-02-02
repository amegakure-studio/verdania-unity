using System;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float speed = 10f;
    private List<Tile> pathVectorList;
    private int currentPathIndex;
    [SerializeField] PathFinder pathFinder;
    [SerializeField] Tile origin;

    void Update()
    {
        HandleMovement();

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
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                Debug.Log(clickType + ": " + tile.coordinate.ToString());
                SetTargetPosition(tile);
            }
        }
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex].gameObject.transform.position;
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving() 
    {
        pathVectorList = null;
    }


    private void SetTargetPosition(Tile targetTile)
    {
        currentPathIndex = 0;
        // TODO: Get character actual tile
        // TODO: Call pathfinder
        //pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        List<Tile> tiles = new();
        tiles.AddRange(pathFinder.FindPath(origin, targetTile).tiles);
        pathVectorList = tiles;

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

}
