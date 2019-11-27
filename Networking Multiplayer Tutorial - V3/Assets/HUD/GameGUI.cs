using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviourPunCallbacks
{
    static GameGUI instance;
    GameObject ui;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI nameText;
 
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        ui = transform.FindAnyChild<Transform>("UI").gameObject;
    }
 
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ui.SetActive(PhotonNetwork.InRoom);
    }
 
    public override void OnJoinedRoom()
    {
        instance.nameText.text = PhotonNetwork.NickName;
    }
 
    public static void SetHealth(float value)
    {
        instance.healthText.text = instance.GetHealthString(value);
    }
 
    public static void SetScore(int value)
    {
        instance.scoreText.text = new System.Text.StringBuilder()
            .Append("Score: ")
            .Append(value)
            .ToString();
    }
 
    string GetHealthString(float normalisedHPPercent)
    {
        return new System.Text.StringBuilder()
            .Append("Health: ")
            .Append((int)(normalisedHPPercent * 100))
            .Append("%")
            .ToString();
    }
}
