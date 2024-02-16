using dojo_bindings;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginController : MonoBehaviour
{
    private TextField usernameTxt;
    private TextField passwordTxt;
    private Button loginBtn;
    private Button signUpBtn;
    private DojoSystem dojoSystem;
    private UpdaterSystem updaterSystem;

    private void Awake()
    {
        
    }

    private void Start()
    {
        dojoSystem = UnityUtils.FindOrCreateComponent<DojoSystem>();
        updaterSystem = UnityUtils.FindOrCreateComponent<UpdaterSystem>();
        VisualElement root = FindObjectOfType<UIDocument>().rootVisualElement;

        usernameTxt = root.Q<TextField>("Username");
        passwordTxt = root.Q<TextField>("Password");

        loginBtn = root.Q<Button>("LoginBtn");
        loginBtn.clicked += LoginBtn_clicked;

        signUpBtn = root.Q<Button>("SignUpBtn");
        signUpBtn.clicked += SignUpBtn_clicked;
    }

    private void SignUpBtn_clicked()
    {
        SceneLoader sceneLoader = UnityUtils.FindOrCreateComponent<SceneLoader>();
        sceneLoader.LoadNextScene();
    }

    private void LoginBtn_clicked()
    {
        try
        {
            SessionCreator sessionCreator = UnityUtils.FindOrCreateComponent<SessionCreator>();    
            Session session = sessionCreator.GetSessionFromExistingPlayer(usernameTxt.text, passwordTxt.text);


            if (session != null)
            {
                try
                {
                    dojo.Call updaterCall = updaterSystem.Connect(session.PlayerId.Hex(), dojoSystem.Systems.updaterSystemAddress);
                    dojoSystem.ExecuteCalls(new[] { updaterCall });

                    SceneLoader sceneLoader = UnityUtils.FindOrCreateComponent<SceneLoader>();
                    sceneLoader.LoadScene(2);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

               
            }
            // TODO: Emmit an event to show some feedback to the user.

        } catch (Exception e) { Debug.Log("Couldn't login. " + e); }        
    }

    private void OnDestroy()
    {
        loginBtn.clicked -= LoginBtn_clicked;
        signUpBtn.clicked -= SignUpBtn_clicked;
    }
}
