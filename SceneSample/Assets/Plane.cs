using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
	public Vector3 pt;
	internal Vector3 normal;

   public void placePlane()
	{
		transform.position = pt;
		transform.rotation = Quaternion.LookRotation(transform.forward, normal);
	}

    public void placePlaneRight()
    {
        transform.position = pt;
        transform.rotation = Quaternion.LookRotation(transform.right, normal);
    }

    // Start is called before the first frame update
    void Start()
	{
		pt = Vector3.up;
		normal = new Vector3(1,2,0);

        placePlane();
		
	}

	// Update is called once per frame
	void Update()
	{

	
	}

	public float distanceTo(Vector3 s)
	{
		return Parallel((pt - s), normal).magnitude;
	}

	public static Vector3 Parallel(Vector3 v, Vector3 n)
	{
		Vector3 norm = n.normalized;
		return Vector3.Dot(v, norm) * norm;
	}



	public static Vector3 Perp(Vector3 v, Vector3 n)
	{
		return v - Parallel(v, n);
	}

	internal Vector3 parallellToSurface(Vector3 v)
	{
		return Perp(v, normal);

	}

	internal Vector3 perpendicularToSurface(Vector3 v)
	{
		return Parallel(v, normal);
	}

    public BouncingScript afterCollision(BouncingScript sphere, Vector3 ptOfImpact,
       Vector3 norm, float coOfRestitution)
    {
        Vector3 perpToNormal = Perp(sphere.velocity, norm);

        Vector3 parallelToNormal = Parallel(sphere.velocity, norm);

        Vector3 newVelocity = perpToNormal - coOfRestitution * parallelToNormal;

        BouncingScript output = sphere;
        output.velocity = newVelocity;

        return output;
    }
}
