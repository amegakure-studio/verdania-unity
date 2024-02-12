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

    public List<TileState> GetTileStatesByFarmID(UInt64 farmid, GameObject[] entities)
    {
        List<TileState> tileStates = new();
        foreach (GameObject go in entities)
        {
            TileState tileState = go.GetComponent<TileState>();

            if (tileState != null)
            {
                if (tileState.farmId == farmid)
                    tileStates.Add(tileState);
            }
        }

        return tileStates;
    }

    public CropState GetCropStateByIndex(UInt64 farmId, UInt64 index, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            CropState cropState = go.GetComponent<CropState>();

            if (cropState != null)
            {
                if (cropState.farmId == farmId && cropState.index == index)
                    return cropState;
            }
        }

        return null;
    }

    public Crop GetCropById(UInt64 id, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            Crop crop = go.GetComponent<Crop>();

            if (crop != null)
            {
                if (crop.id == id)
                    return crop;
            }
        }

        return null;
    }


    public EnvEntityState GetEnvEntityStateByIndex(UInt64 farmId, UInt64 index, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            EnvEntityState envEntityState = go.GetComponent<EnvEntityState>();

            if (envEntityState != null)
            {
                if (envEntityState.farmId == farmId && envEntityState.index == index)
                    return envEntityState;
            }
        }

        return null;
    }

    public EnvEntity GetEnvEntityById(UInt64 id, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            EnvEntity envEntity = go.GetComponent<EnvEntity>();

            if (envEntity != null)
            {
                Debug.Log("envEntity In dojo: " + envEntity.id);
                if (envEntity.id == id)
                    return envEntity;
            }
        }

        return null;
    }

}
