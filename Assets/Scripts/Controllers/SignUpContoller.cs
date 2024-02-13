using System;
using UnityEngine;
using UnityEngine.UIElements;


public class SignUpContoller : MonoBehaviour
{
    private TextField usernameTxt;
    private TextField passwordTxt;
    private Button createBtn;

    void Start()
    {
        // VisualElement root = FindObjectOfType<UIDocument>().rootVisualElement;

        // usernameTxt = root.Q<TextField>("Username");
        // passwordTxt = root.Q<TextField>("Password");

        // createBtn = root.Q<Button>("CreateBtn");
        // createBtn.clicked += CreateBtnBtn_clicked;
    }

    private void CreateBtnBtn_clicked()
    {
        try
        {
            SessionCreator sessionCreator = UnityUtils.FindOrCreateComponent<SessionCreator>();    
            try
            {
                // string username = usernameTxt.text;
                // string password = passwordTxt.text;

                string username = "CrisDojo";
                string password = "Password";

                sessionCreator.CreateNewPlayer(username, password, (SkinType) 1);
                
                // SceneLoader sceneLoader = UnityUtils.FindOrCreateComponent<SceneLoader>();
                // sceneLoader.LoadScene(0);
            }
            catch (Exception e) { Debug.LogError("Error in dojo TX" + e); }        
        
        } catch (Exception e) { Debug.LogError("Couldn't signup! " + e); }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            CreateBtnBtn_clicked();
    }
}
