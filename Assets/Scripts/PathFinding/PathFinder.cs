using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using Amegakure.Verdania.Pathfinding;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Path FindPath(Tile origin, Tile destination)
    {
        List<Tile> openSet = new List<Tile>();
        List<Tile> closedSet = new List<Tile>();
        
        openSet.Add(origin);
        origin.CostFromOrigin = 0;

        float tileDistance = origin.GetComponent<MeshFilter>().sharedMesh.bounds.extents.z * 4;
        
        while (openSet.Count > 0)
        {
            openSet.Sort((x, y) => x.TotalCost.CompareTo(y.TotalCost));
            Tile currentTile = openSet[0];

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            //Destination reached
            if (currentTile == destination)
            {  
                return PathBetween(destination, origin);
            }

            foreach (Tile neighbor in currentTile.GetNeighbors())
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

    public Path PathBetween(Tile dest, Tile source)
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
    private Path MakePath(Tile destination, Tile origin)
    {
        List<Tile> tiles = new List<Tile>();
        Tile current = destination;

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
