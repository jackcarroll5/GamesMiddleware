using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using PUNTutorial;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    Camera playerCamera;
    [SerializeField] NameLabel nameLabel;
    
    public PlayerMovement playerMovement { get; private set; }
    public PlayerShoot playerShoot { get; private set; }
    public PlayerHealth playerHealth { get; private set; }
    public Collider tankCollider { get; private set; }

     public SkeletonScript Skeleton { get; private set; }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetCustomProperty(string propName, object value)
    {
        ExitGames.Client.Photon.Hashtable prop = new ExitGames.Client.Photon.Hashtable();
        prop.Add(propName, value);
        photonView.Owner.SetCustomProperties(prop);
    }
    
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        name = new System.Text.StringBuilder()
            .Append(photonView.Owner.NickName)
            .Append(" [")
            .Append(photonView.ViewID)
            .Append("]")
            .ToString();
 
        BroadcastMessage("OnInstantiate", info, SendMessageOptions.DontRequireReceiver);
    }
    
    
  
    void Awake() {
        DontDestroyOnLoad(gameObject);
        playerCamera = GetComponentInChildren<Camera>();
        //playerMovement = GetComponent<PlayerMovement>();
        Skeleton = GetComponent<SkeletonScript>();
        playerShoot = GetComponent<PlayerShoot>();
        playerHealth = GetComponent<PlayerHealth>();
        tankCollider = GetComponent<Collider>();
        
        if(!photonView.IsMine)
        {
            DestroyImmediate(playerCamera.gameObject);
        }
    }
}
