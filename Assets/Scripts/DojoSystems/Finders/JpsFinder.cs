using Amegakure.Verdania.DojoModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpsFinder : MonoBehaviour
{
    public List<Path>GetPathByPlayerId(string playerId, GameObject[] entities)
    {
        PathCount pathCount = GetPathCountByPlayerId(playerId, entities);

        if(pathCount!= null)
        {
            List<Path> path = new();
            foreach (GameObject go in entities)
            {
                Path point = go.GetComponent<Path>();

                if (point != null)
                {
                    if (point.player.Hex().Equals(playerId) && point.id < pathCount.index)
                        path.Add(point);
                }
            }
            return path;
        }

        return null;
    }

    private PathCount GetPathCountByPlayerId(string playerId, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            PathCount pathCount = go.GetComponent<PathCount>();

            if (pathCount != null)
            {
                if (pathCount.player.Hex().Equals(playerId))
                    return pathCount;
            }
        }

        return null;
    }
}
