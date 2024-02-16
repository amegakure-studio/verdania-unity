using System;
using System.Collections.Generic;
using UnityEngine;

namespace Amegakure.Verdania.GridSystem
{
    public class TileRenderer : MonoBehaviour
    {
        [SerializeField] TileDirectionType tileType = TileDirectionType.ORTHOGONAL;

        #region member fields
        private TileRenderer parent;
        private TileRenderer connectedTile;
        private GameObject occupyingObject;
        private float cost;
        public Vector2Int coordinate;
        private List<Vector2> directions;
        private List<TileRenderer> neighbors = null;
        private bool isMovementTile = true;
        private float costFromOrigin = 0;
        private float costToDestination = 0;
        private int terrainCost = 0;
        private bool occupied = false;
        #endregion

        private void Awake()
        {
            switch (tileType) 
            {
                case TileDirectionType.ORTHOGONAL: 
                    directions = new()
                    {
                        new Vector2(1, 0),
                        new Vector2(0, 1),
                        new Vector2(-1, 0),
                        new Vector2(0, -1),
                    }; break;
                case TileDirectionType.HEXAGONAL:
                    directions = new()
                    {
                        new Vector2(1, 0),
                        new Vector2(1, 1),
                        new Vector2(0, 1),
                        new Vector2(-1, 1),
                        new Vector2(-1, 0),
                        new Vector2(-1, -1),
                        new Vector2(0, -1),
                        new Vector2(1, -1)
                    }; break;
            }
        }

        public GameObject OccupyingObject 
        { get => occupyingObject;
            set 
            { 
                //if (occupyingObject != null)
                //{
                //    foreach (Transform child in transform)
                //    { Destroy(child.gameObject); }
                //}

                GameObject obj = InstantiateObject(value);

                if (obj != null)
                    occupyingObject = obj; 
            } 
        }

        public void AddObject(GameObject prefab)
        {
            InstantiateObject(prefab);
        }

        private GameObject InstantiateObject(GameObject prefab)
        {
            if (prefab != null)
            {
                GameObject prefabGO = Instantiate(prefab);

                Vector3 oldPos = prefabGO.transform.position;
                Vector3 oldScale = prefabGO.transform.localScale;
                Quaternion oldRotation = prefabGO.transform.rotation;

                prefabGO.transform.parent = transform;
                prefabGO.transform.localPosition = oldPos;
                prefabGO.transform.rotation = oldRotation;

                Vector3 inverseScale = new (
                            1.0f / transform.localScale.x,
                            1.0f / transform.localScale.y,
                            1.0f / transform.localScale.z
                            );


                prefabGO.transform.localScale = new Vector3(
                            oldScale.x * inverseScale.x,
                            oldScale.y * inverseScale.y,
                            oldScale.z * inverseScale.z
                        );

                return prefabGO;
            }

            return null;
        }

        public Vector2Int Coordinate { get => coordinate; set => coordinate = value; }
        public bool Occupied { get => occupied; set => occupied = value; }
        public bool InFrontier { get; set; } = false;
        public bool CanBeReached { get { return !Occupied && InFrontier; } }
        public float Cost { get => cost; set => cost = value; }
        public TileRenderer Parent { get => parent; set => parent = value; }
        public TileRenderer ConnectedTile { get => connectedTile; set => connectedTile = value; }
        public bool IsMovementTile { get => isMovementTile; set => isMovementTile = value; }
        public float CostFromOrigin { get => costFromOrigin; set => costFromOrigin = value; }
        public float CostToDestination { get => costToDestination; set => costToDestination = value; }
        public int TerrainCost { get => terrainCost; set => terrainCost = value; }
        public float TotalCost { get { return CostFromOrigin + CostToDestination + TerrainCost; } }

        public List<TileRenderer> GetNeighbors()
        {
            this.neighbors ??= this.CalculateNeighbors(directions);
            return neighbors;
        }


        public List<TileRenderer> GetNeighbors(List<Vector2> directions)
        {
            return this.CalculateNeighbors(directions);
        }

        public List<TileRenderer> CalculateNeighbors(List<Vector2> directions)
        {
            List<TileRenderer> neighbors = new();

            List<Vector2> neighborsDir = new();

            foreach (Vector2 direction in directions)
            {
                neighborsDir.Add(coordinate + direction);
            }

            GameObject parent = transform.parent.gameObject;

            TileRenderer[] tiles = parent.GetComponentsInChildren<TileRenderer>();

            foreach (TileRenderer tile in tiles)
            {
                if (neighborsDir.Contains(tile.coordinate))
                    neighbors.Add(tile);

                if (neighbors.Count == 8)
                    break;
            }

            if (this.connectedTile != null)
                neighbors.Add(connectedTile);

            return neighbors;
        }
    }
}

