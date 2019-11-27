using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

namespace PUNTutorial
{
    public class PlayerHealth : MonoBehaviourPunCallbacks
    {
        [SerializeField] HealthBar healthBar;
        [SerializeField] TankExploder tankExploderPrefab;
        
        const int MAX_HP = 100;
        
        Player _player;
        Player player
        {
            get
            {
                if (_player == null) _player = GetComponent<Player>();
                return _player;
            }
        }
        
        int hitPoints
        {
            get
            {
                object hp;
                if (photonView.Owner.CustomProperties.TryGetValue("HP", out hp))
                    return (int) hp;
                else
                    return MAX_HP;
            }
            set
            {
                player.SetCustomProperty("HP", value);
            }
        }
        
        public override void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (photonView.IsMine)
            {
                hitPoints = MAX_HP;
            }  
            else
                DisplayHealth();
        }

        float GetNormalisedHealthPercent(int hp)
        {
            return hp / (float)MAX_HP;
        }
        
        
        void DisplayHealth()
        {
            healthBar.SetHealthBarValue(GetNormalisedHealthPercent(hitPoints));
            if (photonView.IsMine)
            {
                GameGUI.SetHealth(GetNormalisedHealthPercent(hitPoints));
            }
        } 
        
        public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
        {
            var plyr = (Photon.Realtime.Player) playerAndUpdatedProps[0];
            if (plyr.ActorNumber == photonView.OwnerActorNr)
            {
                var props = (ExitGames.Client.Photon.Hashtable) playerAndUpdatedProps[1];
                if (props.ContainsKey("HP"))
                    DisplayHealth();
            }
        }
        
        IEnumerator ShowTank(float delay)
        {
            yield return new WaitForSeconds(delay);
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        [PunRPC]
       void RPC_RespawnTank(short[] respawnPos, short rot)
        {
            if(photonView.IsMine)
            {
                player.playerMovement.enabled = true;
                player.playerShoot.enabled = true;
            }
            player.tankCollider.enabled = true;
            transform.position = transform.position.FromShortXZ(respawnPos);
            transform.rotation = Quaternion.Euler(0, rot, 0);
            hitPoints = MAX_HP;
            StartCoroutine(ShowTank(photonView.IsMine ? 0f:0.25f));
        }
        IEnumerator ExplodeTank()
        {
            photonView.RPC("RPC_ExplodeTank", RpcTarget.All);
            yield return new WaitForSeconds(7f);
            var spawnPoint = GameMan.Instance.GetClearSpawnPoint();
            photonView.RPC("RPC_RespawnTank", RpcTarget.All, spawnPoint.position.ToShortXZ(), (short)spawnPoint.rotation.eulerAngles.y);
        }
        
        [PunRPC]
       public void RPC_ExplodeTank()
        {
            player.playerMovement.enabled = false;
            player.playerShoot.enabled = false;
            player.tankCollider.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            Instantiate(tankExploderPrefab, transform.position, transform.rotation);
        }

        public void DoDamage(Missile missile)
        {
            hitPoints = Mathf.Clamp(hitPoints - missile.damage, 0 , MAX_HP);
            if (hitPoints == 0)
            {
                missile.MissileOwner.photonView.RPC("RPC_AddScore", missile.MissileOwner.photonView.Owner, 1);
                StartCoroutine(ExplodeTank());
            }
        }

       public string GetHealthString()
        {
            return new System.Text.StringBuilder()
                .Append("Health = ")
                .Append(hitPoints / (float)MAX_HP * 100)
                .Append("%")
                .ToString();
        }
    } 
}
