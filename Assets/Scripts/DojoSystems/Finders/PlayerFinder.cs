using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public PlayerState GetPlayerStateById(string hexId, GameObject[] entities)
    {
        foreach (GameObject go in entities)
        {
            PlayerState playerState = go.GetComponent<PlayerState>();

            if (playerState != null)
            {
                if (playerState.player.Hex().Equals(hexId))
                    return playerState;
            }
        }

        return null;
    }

    public List<ERC1155Balance> GetPlayerItems(string hexId, GameObject[] entities)
    {
        List<ERC1155Balance> items = new();

        foreach (GameObject go in entities)
        {
            ERC1155Balance item = go.GetComponent<ERC1155Balance>();

            if (item != null)
            {
                if (item.account.Hex().Equals(hexId))
                    items.Add(item);
            }
        }

        return items;
    }

}
