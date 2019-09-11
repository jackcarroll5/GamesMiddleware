using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BouncingScript : MonoBehaviour
{
    private float floorHeight = 0;
    private float gravity = 9.8f;
    private Vector3 velocity;
    private Vector3 acceleration;
    private int hasBounce = 10;

    /*[SerializeField]
    private int loopBounce = 10;*/

    [SerializeField]
    private float coOfRestitution = 0.6f;

    private float radius;
    private Vector3 previousVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
       acceleration = Vector3.down * gravity;
        previousVelocity = velocity;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (transform.position.y <= floorHeight + radius)
        {

            transform.position -= velocity * Time.deltaTime;//Undoes the velocity

            Debug.Log("Bounce No " + hasBounce);
           
            velocity = -velocity; //Perfectly Elastic Velocity

            velocity = coOfRestitution * velocity;

            //transform.position -= velocity;

            hasBounce++;

        }
        
    }

    public static Vector3 Parallel(Vector3 v, Vector3 normal)
    {
        Vector3 norm = normal.normalized;
        return Vector3.Dot(v, norm) * norm;
    }

    public static Vector3 Arp(Vector3 v, Vector3 n)
    {
        return v - Parallel(v, n);
    }

    private void OnValidate()
    {
        radius = transform.localScale.y / 2f;
    }
}
