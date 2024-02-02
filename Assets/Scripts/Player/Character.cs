using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    private const float speed = 3f;
    [SerializeField] Tile initTile;

    private List<Tile> pathVectorList;
    private int currentPathIndex;
    private Tile currentTile;

    public List<Tile> PathVectorList
    {
        get => pathVectorList;
        set
        {
            pathVectorList = value;

            if (pathVectorList != null && pathVectorList.Count > 1)
            {
                pathVectorList.RemoveAt(0);
            }
        }
    }
    public int CurrentPathIndex { get => currentPathIndex; set => currentPathIndex = value; }
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }

    private void Start()
    {
        if (initTile != null)
            CurrentTile = initTile;
    }

    public void HandleMovement()
    {
        if (PathVectorList != null)
        {
            Tile lastTile = PathVectorList[CurrentPathIndex];
            Vector3 targetPosition = lastTile.gameObject.transform.position;
            
            if (Vector3.Distance(transform.position, targetPosition) > 0.3f)
            {
                MoveAndRotate(targetPosition);
                this.currentTile = lastTile;
            }
            else
            {
                CurrentPathIndex++;
                if (CurrentPathIndex >= PathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void MoveAndRotate(Vector3 destination)
    {
        Vector3 moveDir = (destination - transform.position).normalized;

        float distanceBefore = Vector3.Distance(transform.position, destination);
        transform.position = transform.position + moveDir * speed * Time.deltaTime;
                
        Vector3 vectorRotation = transform.position.DirectionTo(destination).Flat();

        if (vectorRotation != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(vectorRotation, Vector3.up);
    }

    private void StopMoving()
    {
        Tile lastTile = PathVectorList[PathVectorList.Count - 1];
        this.currentTile = lastTile;
        PathVectorList = null;
    }
}
