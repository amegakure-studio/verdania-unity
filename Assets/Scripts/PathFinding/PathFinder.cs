using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using Amegakure.Verdania.Pathfinding;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Path FindPath(TileRenderer origin, TileRenderer destination)
    {
        List<TileRenderer> openSet = new List<TileRenderer>();
        List<TileRenderer> closedSet = new List<TileRenderer>();
        
        openSet.Add(origin);
        origin.CostFromOrigin = 0;

        float tileDistance = origin.GetComponent<MeshFilter>().sharedMesh.bounds.extents.z * 4;
        
        while (openSet.Count > 0)
        {
            openSet.Sort((x, y) => x.TotalCost.CompareTo(y.TotalCost));
            TileRenderer currentTile = openSet[0];

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            //Destination reached
            if (currentTile == destination)
            {  
                return PathBetween(destination, origin);
            }

            foreach (TileRenderer neighbor in currentTile.GetNeighbors())
            {
                if(closedSet.Contains(neighbor))
                    continue;
                
                float costToNeighbor = currentTile.CostFromOrigin + neighbor.TerrainCost + tileDistance;
                if (costToNeighbor < neighbor.CostFromOrigin || !openSet.Contains(neighbor))
                {
                    neighbor.CostFromOrigin = costToNeighbor;
                    neighbor.CostToDestination = Vector3.Distance(destination.transform.position, neighbor.transform.position);
                    neighbor.Parent = currentTile;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    public Path PathBetween(TileRenderer dest, TileRenderer source)
    {
        Path path = MakePath(dest, source);
        return path;
    }

    /// <summary>
    /// Creates a path between two tiles
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    private Path MakePath(TileRenderer destination, TileRenderer origin)
    {
        List<TileRenderer> tiles = new List<TileRenderer>();
        TileRenderer current = destination;

        while (current != origin)
        {
            tiles.Add(current);
            if (current.Parent != null)
                current = current.Parent;
            else
                break;
        }

        tiles.Add(origin);
        tiles.Reverse();

        Path path = new Path();
        path.tiles = tiles.ToArray();

        return path;
    }
}
