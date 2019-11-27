using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using PUNTutorial;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] ParticleSystem trail;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] Rigidbody rbody;
    public int damage = 10;
    float speed = 60f;
    public PlayerShoot MissileOwner { get; private set; }
 
    void FixedUpdate()
    {
        rbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
 
    public void SetOwner(PhotonView ownerView)
    {
        MissileOwner = ownerView.GetComponent<PlayerShoot>();
    }
 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var hitPlayer = other.GetComponent<PlayerHealth>();
 
            if (hitPlayer.photonView.IsMine)
            {
                if (MissileOwner.photonView.ViewID == hitPlayer.photonView.ViewID) 
                return;

                hitPlayer.DoDamage(this);
                MissileOwner.photonView.RPC("RPC_AddScore", MissileOwner.photonView.Owner, 25);
            }
        }
        DestroyMissile();
    }
 
 
    void DestroyMissile()
    {
        trail.transform.SetParent(null);
        trail.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        explosion.transform.SetParent(null);
        explosion.Play();
        Destroy(explosion.gameObject, 3f);
        Destroy(gameObject);
    }
 }
