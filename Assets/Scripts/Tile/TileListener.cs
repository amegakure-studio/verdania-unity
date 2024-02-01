using System.Collections;
using System.Collections.Generic;
using Amegakure.Verdania.GridSystem;
using UnityEngine;

public class TileListener : MonoBehaviour
{
    void Update()
    {
        string clickType = Input.GetMouseButtonDown(0) ? "Left" :
                    Input.GetMouseButtonDown(1) ? "Right" : null;
        
        if(clickType != null)
            HandleClick(clickType);
    }

    private void HandleClick(string clickType)
    {
        Vector2 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        int layerMask = 1 << LayerMask.NameToLayer("Tile"); 
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != null)
            {
                Debug.Log(clickType + ": " + tile.coordinate.ToString());
            }
        }
    }
}
