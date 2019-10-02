using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float speedHorizontal = 10.0f;
    float speedVertical = 10.0f;

    float yawing = 1.0f;
    float pitching = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKey("w"))
        {
            transform.position += Vector3.up;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left;
        }
        if (Input.GetKey("s"))
        {
            transform.position += Vector3.down;
        }
         if (Input.GetKey("d"))
        {
            transform.position += Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        float turnVertical = Input.GetAxis("Mouse Y");
        float turnHorizontal = Input.GetAxis("Mouse X");

      
        yawing += speedHorizontal * turnHorizontal;
        pitching -= speedVertical * turnVertical;

      // transform.eulerAngles = new Vector3(pitching, yawing, 0.0f);

 }
}
