using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMan : MonoBehaviourPunCallbacks
{
    GameObject defaultSpawnPoint;
    public static GameMan Instance;
    public static GameObject localPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        
        defaultSpawnPoint = new GameObject("Default SpawnPoint");
        defaultSpawnPoint.transform.position = new Vector3(0,0.5f,0);
        defaultSpawnPoint.transform.SetParent(transform,false);

        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public void JoinGame()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 6;
        PhotonNetwork.NickName = MainMenu.NickName;
        PhotonNetwork.JoinOrCreateRoom("Default Room",  ro, null);
        PlayerPrefs.SetString("NickName", MainMenu.NickName);

        OnJoinedRoom();
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!PhotonNetwork.InRoom) 
            return;
        
        var spawnPoint = GetRandomSpawnPoint();
 
        /*localPlayer = PhotonNetwork.Instantiate(
            "Player",
            spawnPoint.position,
            spawnPoint.rotation, 0);*/

            localPlayer = PhotonNetwork.Instantiate(
            "skeleton_animated Variant",
            spawnPoint.position,
            spawnPoint.rotation, 0);
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Welcome to the Room");
        
        if(PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel("MainGame");
            //SceneManager.LoadScene("MainGame");
        }
        
        
    }

    public Transform GetClearSpawnPoint()
    {
        var spawnPoints = GetAllObjectsOfTypeInScene<SpawnPts>();
        while (spawnPoints.Count > 0)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            //var mask = LayerMask.GetMask("Player");
             var mask = LayerMask.GetMask("skeleton_animated Variant");
            Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.transform.position, 2, mask);
            if (hitColliders.Length == 0)
            {
                return spawnPoint.transform;
            }
            else
            {
                spawnPoints.Remove(spawnPoint);
            }
        }
        return GetRandomSpawnPoint();
    }


    public Transform GetRandomSpawnPoint()
    {
        var spawnPoints = GetAllObjectsOfTypeInScene<SpawnPts>();
        if(spawnPoints.Count == 0)
        {
            return defaultSpawnPoint.transform;
        }
        else
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
        }
    }
    
    public static List<GameObject> GetAllObjectsOfTypeInScene<T>()
    {
        List<GameObject> objectsInScene = new List<GameObject>();
 
        foreach (GameObject go in (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.hideFlags == HideFlags.NotEditable ||
                go.hideFlags == HideFlags.HideAndDontSave)
                continue;
 
            if (go.GetComponent<T>() != null)
                objectsInScene.Add(go);      
        }
 
        return objectsInScene;
    }
    
}
