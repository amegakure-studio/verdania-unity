using Amegakure.Verdania.DojoModels;
using Dojo;
using UnityEngine;

public class EntityUpdateNotifier : MonoBehaviour
{
    private SynchronizationMaster synchronizationMaster;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        synchronizationMaster = GameObject.FindObjectOfType<SynchronizationMaster>();
        synchronizationMaster.OnEntitySpawned.AddListener(HandleEntitySpawned);
    }

    private void HandleEntitySpawned(GameObject go)
    {
        ModelInstance[] models = go.GetComponents<ModelInstance>();

        foreach (ModelInstance model in models)
        {
            if (model.GetType().Equals(typeof(TileState)))
            {
                EventManager.Instance.Publish(GameEvent.SPAWN_MAPELEMENT, new() { { "Element", model } });
            }
        }
        
    }
}
