using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginController : MonoBehaviour
{
    private TextField usernameTxt;
    private TextField passwordTxt;
    private Button loginBtn;

    private void Start()
    {
        VisualElement root = FindObjectOfType<UIDocument>().rootVisualElement;

        usernameTxt = root.Q<TextField>("Username");
        passwordTxt = root.Q<TextField>("Password");

        loginBtn = root.Q<Button>("LoginBtn");
        loginBtn.clicked += LoginBtn_clicked;
    }

    private async void LoginBtn_clicked()
    {
        try
        {
            SessionCreator sessionCreator = UnityUtils.FindOrCreateComponent<SessionCreator>();    
            await sessionCreator.Create(usernameTxt.text, passwordTxt.text);

            SceneLoader sceneLoader = UnityUtils.FindOrCreateComponent<SceneLoader>();
            sceneLoader.LoadNextScene();

        } catch (Exception e) { Debug.Log("Couldn't login. " + e); }
        
    }

    private void OnDestroy()
    {
        loginBtn.clicked -= LoginBtn_clicked;
    }
}
