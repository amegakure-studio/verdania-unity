using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "ScriptableObjects/PrefabData", order = 1)]
public class PrefabData : ScriptableObject
{
    [System.Serializable]
    public struct ObjectPrefabPair
    {
        public string objectName;
        public GameObject prefab;
    }

    public ObjectPrefabPair[] objectPrefabPairs;
}
