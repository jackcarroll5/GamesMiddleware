using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

    
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NameLabel : MonoBehaviour {
 
     public void OnInstantiate(PhotonMessageInfo info)
        {
            var pView = GetComponentInParent<PhotonView>();

            if (pView.IsMine)
            {
                gameObject.SetActive(false);
                return;
            }
 
            GetComponent<TextMeshProUGUI>().text = pView.Owner.NickName;
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
