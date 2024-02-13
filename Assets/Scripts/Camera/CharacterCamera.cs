using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEvent.PLAYER_CREATED, HandlePlayerCreated);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEvent.PLAYER_CREATED, HandlePlayerCreated);
    }

    private void HandlePlayerCreated(Dictionary<string, object> context)
    {
        try
        {
            Character character = (Character)context["Player"];

            CinemachineFreeLook camera = GameObject.FindObjectOfType<CinemachineFreeLook>();
            camera.Follow = character.transform;
            camera.LookAt = character.Head;

        }
        catch { }
    }
}
