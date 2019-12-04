using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerShoot : MonoBehaviourPunCallbacks
{
    
    [SerializeField] Missile missilePrefab;
    [SerializeField] Transform firePos;
    float nextFireTime;
    float fireDelay = 0.3f;

    void Awake()
    {
        enabled = photonView.IsMine;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    [PunRPC]
    void RPC_FireMissile(PhotonMessageInfo info)
    {
        var missile = Instantiate(missilePrefab, firePos.position, transform.rotation);
        missile.SetOwner(info.photonView);
        missile.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextFireTime) 
            return;

        if (Input.GetAxis("Fire1") > 0)
        {
            nextFireTime = Time.time + fireDelay;
            photonView.RPC("RPC_FireMissile", RpcTarget.All);
        }
    }
}
