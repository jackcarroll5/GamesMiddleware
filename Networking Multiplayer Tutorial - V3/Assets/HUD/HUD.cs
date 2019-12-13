using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    static HUD instance;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake () 
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
