using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Dictionary<GameObject, Animator> characterAnimatorMap;

    private void Awake()
    {
        characterAnimatorMap = new Dictionary<GameObject, Animator>();
    }

    private void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEvent.CHARACTER_MOVE_START, HandleMove_start);
        EventManager.Instance.Subscribe(GameEvent.CHARACTER_MOVE_END, HandleMove_end);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_MOVE_START, HandleMove_start);
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_MOVE_END, HandleMove_end);
    }

    private void HandleMove_start(Dictionary<string, object> context)
    {
        try
        {
            GameObject character = (GameObject)context["Character"];

            if (!characterAnimatorMap.ContainsKey(character))
            {
                Animator animator = character.GetComponentInChildren<Animator>();

                if (animator != null)
                    characterAnimatorMap.Add(character, animator);
            }

            else
            {
                Animator animator = characterAnimatorMap[character];
                animator.SetBool("IsWalking", true);
            }
        }

        catch (Exception e) { Debug.LogException(e); }

    }

    private void HandleMove_end(Dictionary<string, object> context)
    {
        try
        {
            GameObject character = (GameObject)context["Character"];

            if (!characterAnimatorMap.ContainsKey(character))
            {
                Animator animator = character.GetComponentInChildren<Animator>();

                if (animator != null)
                    characterAnimatorMap.Add(character, animator);
            }

            else
            {
                Animator animator = characterAnimatorMap[character];
                animator.SetBool("IsWalking", false);
            }
        }

        catch (Exception e) { Debug.LogException(e); }
    }

}