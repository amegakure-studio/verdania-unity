using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        StartCoroutine(nameof(LoadAsyncScene));
    }

    IEnumerator LoadAsyncScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex + 1);
            asyncLoad.allowSceneActivation = false;

            EventManager.Instance.Publish(GameEvent.GAME_LOADING_START, new Dictionary<string, object>());

            while (!asyncLoad.isDone)
            {
                //Debug.Log("Still here: " + asyncLoad.progress);
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
