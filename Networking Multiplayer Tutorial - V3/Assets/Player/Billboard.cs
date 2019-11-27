using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] string cameraTag;
 
    Player _player;
    Player player
    {
        get
        {
            if (_player == null) _player = GetComponentInParent<Player>();
            return _player;
        }
    }
 
    Camera _sceneCamera;
    Camera sceneCamera
    {
        get
        {
            if (_sceneCamera == null) 
                _sceneCamera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
            return _sceneCamera;
        }
    }
 
    void Update()
    {
        transform.LookAt(sceneCamera.transform);
        var rot = transform.rotation.eulerAngles;
        rot.x = 0;
        rot.y -= 180;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
