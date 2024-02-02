using System.Collections;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class DynamicObjectCreator : MonoBehaviour
{
    [System.Serializable]
    public struct MapObjectTilePair
    {
        public string objectName;
        public Tile tile;
    }

    [SerializeField] MapObjectTilePair[] mapObjectTilePair;
    
    [SerializeField] PrefabData prefabData;

    void Start()
    {
        foreach (MapObjectTilePair _object in mapObjectTilePair)
        {
            CreatePrefab(_object.tile.transform, _object.objectName);
        }
    }

    public void CreatePrefab(Transform parentTransform, string prefabName)
    {
        GameObject prefabToInstantiate = GetPrefabByName(prefabName);
        if(prefabToInstantiate != null)
        {
            GameObject prefabGO = Instantiate(prefabToInstantiate);
        
            Vector3 oldPos = prefabGO.transform.position;
            Vector3 oldScale = prefabGO.transform.localScale;
            Quaternion oldRotation = prefabGO.transform.rotation;
        
            prefabGO.transform.parent = parentTransform;
            prefabGO.transform.localPosition = oldPos;
            prefabGO.transform.rotation = oldRotation;

            Vector3 inverseScale = new Vector3(
                        1.0f / parentTransform.localScale.x,
                        1.0f / parentTransform.localScale.y,
                        1.0f / parentTransform.localScale.z
                        );
            

            prefabGO.transform.localScale = new Vector3(
                        oldScale.x * inverseScale.x,
                        oldScale.y * inverseScale.y,
                        oldScale.z * inverseScale.z
                    );
        }
    }

    GameObject GetPrefabByName(string objectName)
    {
        foreach (var pair in prefabData.objectPrefabPairs)
        {
            if (pair.objectName == objectName)
            {
                return pair.prefab;
            }
        }
        return null;
    }
}
