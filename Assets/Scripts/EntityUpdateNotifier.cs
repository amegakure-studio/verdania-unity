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
        Debug.Log("Entity spawn: " );
        ModelInstance[] models = go.GetComponents<ModelInstance>();

        foreach (ModelInstance model in models)
        {
            Debug.Log("TyPE: " + model.GetType());
            if (model.GetType().Equals(typeof(EnvEntityState)))
            {
                Debug.Log("New Env state!!!!");
                EventManager.Instance.Publish(GameEvent.SPAWN_ENVELEMENT, new() { { "Element", model } });
            }

            else if (model.GetType().Equals(typeof(CropState)))
            {
                EventManager.Instance.Publish(GameEvent.SPAWN_CROP, new() { { "Element", model } });
            }

            else if (model.GetType().Equals(typeof(ERC1155Balance)))
            {
                EventManager.Instance.Publish(GameEvent.SPAWN_ERC1155BALANCE, new() { { "Item", model } });
            }
        }
        
    }
}
