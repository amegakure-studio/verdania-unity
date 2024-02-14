using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(nameof(LoadAsyncScene), currentSceneIndex + 1);
        }
    }

    public void LoadScene(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(nameof(LoadAsyncScene), index);
        }
    }

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
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
