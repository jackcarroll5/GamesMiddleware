using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    GameObject ui;
    Button joinGameButton;
    TMP_InputField nicknameInputField;

    public static string NickName {
         get 
         { 
             return instance.nicknameInputField.text; 
         }
    }

    static MainMenu instance;
    
    public override void OnConnectedToMaster()
    {
        joinGameButton.interactable = true;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ui.SetActive(!PhotonNetwork.InRoom);
    }
    
    public override void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    void Awake () {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        
        ui = transform.FindAnyChild<Transform>("UI").gameObject;
        joinGameButton = transform.FindAnyChild<Button>("JoinGame");
        
      
        joinGameButton.interactable = true;
        ui.SetActive(true);

        //nicknameInputField = transform.FindAnyChild<TMP_InputField>("NameInputField(TMP)");
        nicknameInputField = GetComponentInChildren<TMP_InputField>();
        nicknameInputField.text = PlayerPrefs.GetString("NickName", "Player");
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
