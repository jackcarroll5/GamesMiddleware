using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BouncingScript : MonoBehaviour
{
	private float floorHeight = 0;
	private float gravity = 9.8f;

	public Vector3 velocity;
    private Vector3 newVelocity;

	private Vector3 acceleration;
    private float distance;
	private int hasBounce = 10;


    public float mass = 1f;

	/*[SerializeField]
	private int loopBounce = 10;*/

	[SerializeField]
	public float coOfRestitution = 0.6f;


	public float radius {
        get
        {
            return transform.localScale.x / 2f;
        }
        set {
            transform.localScale = new Vector3(2 * value, 2 * value, 2 * value);
        }
    }

    
	private Vector3 previousVelocity = Vector3.zero;
    Vector3 pt;

    internal bool isColliding(Plane p, ref float dist)
    {
        dist = p.distanceTo(transform.position) - radius;
        return dist < 0;
    }

    // Start is called before the first frame update
    void Start()
	{
		OnValidate();
	}

	// Update is called once per frame
	void Update()
	{     
	    acceleration = Vector3.down * gravity;
		previousVelocity = velocity;
		velocity += acceleration * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;
    }

	private void OnValidate()
	{
		radius = transform.localScale.y / 2f;
	}

    public Vector3 getNormal(Vector3 v)
    {
        return (v - transform.position).normalized;
    }

    public bool isColliding(BouncingScript sphere)
    {
        return Vector3.Distance(transform.position, sphere.transform.position) < (radius + sphere.radius);
    }

    public Vector3 ptOfImpact(BouncingScript sphere)
    {
        if(!isColliding(sphere))
        {
            throw new Exception("No collision");
        }

        Vector3 dirOfPtOfImpactFromCentre = Vector3.Normalize(sphere.transform.position - transform.position);

        return transform.position + radius * dirOfPtOfImpactFromCentre;
    }

    public Vector3 normForCollision(BouncingScript sphere)
    {
        return Vector3.Normalize(sphere.transform.position - transform.position);
    }

}
