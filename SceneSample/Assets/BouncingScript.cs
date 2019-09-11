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
   

	private Plane plane;

	/*[SerializeField]
	private int loopBounce = 10;*/

	[SerializeField]
	private float coOfRestitution = 0.6f;

	private float radius;
	private Vector3 previousVelocity = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{
		plane = FindObjectOfType<Plane>();
		OnValidate();
	}

	// Update is called once per frame
	void Update()
	{     
	   acceleration = Vector3.down * gravity;
		previousVelocity = velocity;
		velocity += acceleration * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;

		if (plane.distanceTo(transform.position) < radius)
		{
			Vector3 parallel_to_surface = plane.parallellToSurface(velocity);
			Vector3 perpendicular_to_surface = plane.perpendicularToSurface(velocity);
			//transform.position -= velocity * Time.deltaTime;//Undoes the velocity

			Debug.Log("Bounce No " + hasBounce);

			velocity = parallel_to_surface - perpendicular_to_surface;

		  //  velocity = coOfRestitution * velocity;

			//transform.position -= velocity;

			hasBounce++;
		}      
	}

	private void OnValidate()
	{
		radius = transform.localScale.y / 2f;
	}
}
