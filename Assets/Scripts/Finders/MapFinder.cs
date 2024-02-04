using Amegakure.Verdania.DojoModels;
using Dojo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFinder : MonoBehaviour
{
    public List<Tile> GetTilesByMapID(UInt64 mapid, GameObject[] entities)
    {
        List<Tile> tiles = new();
        foreach (GameObject go in entities)
        {
            Tile tile = go.GetComponent<Tile>();
            
            if(tile != null)
            {
                if(tile.mapID == mapid)
                    tiles.Add(tile);
            }
        }

        return tiles;
    }
}
