using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class SignUpContoller : MonoBehaviour
{
    [SerializeField] CharacterMap[] characterMap;

    private TextField usernameTxt;
    private TextField passwordTxt;
    private EnumField characterDropdown;
    private Button signupBtn;

    void Start()
    {
        VisualElement root = FindObjectOfType<UIDocument>().rootVisualElement;

        usernameTxt = root.Q<TextField>("Username");
        passwordTxt = root.Q<TextField>("Password");
        
        characterDropdown = root.Q<EnumField>("Character");
        characterDropdown.RegisterValueChangedCallback<Enum>(CharacterChanged);

        signupBtn = root.Q<Button>("SignupBtn");
        signupBtn.clicked += SignupBtn_clicked;
    }

    private void CharacterChanged(ChangeEvent<Enum> evt)
    {
        SkinType skintype = (SkinType)characterDropdown.value;

        characterMap.ToList().ForEach(cMap => cMap.characterGo.SetActive(false));
        characterMap.ToList().Find(cMap => cMap.skinType == skintype).characterGo.SetActive(true);
    }

    private void SignupBtn_clicked()
    {
        try
        {
            SessionCreator sessionCreator = UnityUtils.FindOrCreateComponent<SessionCreator>();    
            try
            {
                string username = usernameTxt.text;
                string password = passwordTxt.text;

                sessionCreator.CreateNewPlayer(username, password, (SkinType)characterDropdown.value);

                SceneLoader sceneLoader = UnityUtils.FindOrCreateComponent<SceneLoader>();
                sceneLoader.LoadScene(0);
            }
            catch (Exception e) { Debug.LogError("Error in dojo TX" + e); }        
        
        } catch (Exception e) { Debug.LogError("Couldn't signup! " + e); }        
    }

    [Serializable]
    private struct CharacterMap
    {
        public GameObject characterGo;
        public SkinType skinType;
    }
}
