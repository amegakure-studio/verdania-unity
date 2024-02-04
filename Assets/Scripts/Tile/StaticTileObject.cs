using UnityEngine;

namespace Amegakure.Verdania.GridSystem
{
    public class StaticTileObject : MonoBehaviour
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.TryGetComponent<TileRenderer>(out TileRenderer tile);
            if (tile != null)
            {
                tile.OccupyingObject = gameObject;
                rb.detectCollisions = false;
                rb.isKinematic = true;
            }
        }   
    }
}
