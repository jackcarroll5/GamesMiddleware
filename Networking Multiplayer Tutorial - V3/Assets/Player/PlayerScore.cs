using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

    public class PlayerScore : MonoBehaviourPunCallbacks
    {
        int score;

        [PunRPC]
        void RPC_AddScore(int amount)
        {
            score += amount;
            
            if(photonView.IsMine)
            {
                GameGUI.SetScore(score);
            }
            Debug.Log("Score: " + score);
        }
    } 

