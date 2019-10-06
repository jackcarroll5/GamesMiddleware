using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BouncingScript : MonoBehaviour
{

	private float gravity = 9.8f;

	public Vector3 velocity;
    private Vector3 newVelocity;

    [SerializeField]
    private Vector3 initVelocity;

    [SerializeField]
    private float minimumVelocity = 11f;

    private Vector3 acceleration;
    private float distance;
    private float speed;
    private float time;

    private Vector3 lastFrameVel;


    public float mass = 1f;

	/*[SerializeField]
	private int loopBounce = 10;*/

	[SerializeField]
	public float coOfRestitution;


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

        radius = mass * UnityEngine.Random.Range(0.1f,0.35f);

        transform.localScale += new Vector3(radius, radius, radius);
	}

	// Update is called once per frame
	void Update()
	{     
	    acceleration = Vector3.down * gravity;
		previousVelocity = velocity;
		velocity += acceleration * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;


    }

    private void OnEnable()
    {
        velocity = initVelocity;
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


    public void Bouncer(Vector3 colNormal)
    {
        var speed = previousVelocity.magnitude;
        Debug.Log("Speed: " + speed);

        var direction = Vector3.Reflect(previousVelocity.normalized, colNormal);
        Debug.Log("Out Direction: " + direction);

        velocity = direction * Mathf.Max(speed, minimumVelocity);
        Debug.Log("Velocity: " + velocity);

        float time = Time.deltaTime;

        distance = speed * time;
        Debug.Log("Distance: " + distance);

        time = distance / speed;
        Debug.Log("Time Of Impact Simplified: " + time);

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
