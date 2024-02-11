using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        EventManager.Instance.Subscribe(GameEvent.CHARACTER_WATER, HandleWater);
        EventManager.Instance.Subscribe(GameEvent.CHARACTER_PLANT, HandlePlant);
        EventManager.Instance.Subscribe(GameEvent.CHARACTER_HOE, HandleHoe);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_MOVE_START, HandleMove_start);
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_MOVE_END, HandleMove_end);
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_WATER, HandleWater);
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_PLANT, HandlePlant);
        EventManager.Instance.Unsubscribe(GameEvent.CHARACTER_HOE, HandleHoe);
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

    private void HandlePlant(Dictionary<string, object> context)
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
                ActivateTool("Bag", character);  
                animator.SetTrigger("Plant");
            }
        }

        catch (Exception e) { Debug.LogException(e); }

    }

    private void HandleHoe(Dictionary<string, object> context)
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
                ActivateTool("Hoe", character);
                animator.SetTrigger("Hoe");
            }
        }

        catch (Exception e) { Debug.LogException(e); }

    }


    private void HandleWater(Dictionary<string, object> context)
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
                ActivateTool("WateringCan", character);
                animator.SetTrigger("Water");
            }
        }

        catch (Exception e) { Debug.LogException(e); }

    }

    private void ActivateTool(string toolName, GameObject character)
    {
        Transform toolsParent = TransformDeepChildExtension.FindDeepChild(character.transform, "Tools");

        for (int i = 0; i < toolsParent.childCount; i++)
        {
            toolsParent.GetChild(i).gameObject.SetActive(toolsParent.GetChild(i).name == toolName);
        }
    }
}

public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    /*
	//Depth-first search
	public static Transform FindDeepChild(this Transform aParent, string aName)
	{
		foreach(Transform child in aParent)
		{
			if(child.name == aName )
				return child;
			var result = child.FindDeepChild(aName);
			if (result != null)
				return result;
		}
		return null;
	}
	*/
}