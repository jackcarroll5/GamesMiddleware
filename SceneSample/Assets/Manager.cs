using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

	private float radius;
    private float radius1;

	Plane[] allPlanes;
	BouncingScript[] allSpheres;

	// Start is called before the first frame update
	void Start()
	{
		allPlanes = FindObjectsOfType<Plane>();
		allSpheres = FindObjectsOfType<BouncingScript>();
	}

	private void OnValidate()
	{
		if(allSpheres == null)
		{
			allSpheres = FindObjectsOfType<BouncingScript>();
		}

        radius = transform.localScale.y / 2f;
        radius1 = transform.localScale.y / 2f;
    }

   
	private class Collisions
	{

	}

	// Update is called once per frame
	private void Update()
	{
		for (int i = 0; i < allSpheres.Length-1; i++)
		{
		   BouncingScript spheres = allSpheres[i];

			foreach (Plane p in allPlanes)
			{
				if(spheres.distanceTo(p.transform.position) < radius)
				{
					Debug.Log("Collision occurs between Sphere " + spheres + "and Plane " + p);
				}

			}

			for (int j = i + 1; j < allSpheres.Length; j++)
			{
			
				BouncingScript sphere1 = allSpheres[j];

				var otherPos = sphere1.transform.position;

                sphere1.getNormal(otherPos);

                Debug.Log("Normal of sphere 1 is: " + sphere1.getNormal(otherPos));

                if (spheres.distanceTo(sphere1.transform.position) < radius + radius1)
				{
                    if(spheres.isColliding(sphere1))
                    {
					Debug.Log("Collision occurs between Sphere " + spheres + "and Sphere " + sphere1);
                    }
                }

				Vector3 norm = spheres.getNormal(otherPos);


				var s1VelocityParallel = Plane.Parallel(spheres.velocity,norm);
				var s1VelocityPerp = Plane.Perp(spheres.velocity, norm);

				var s2VelocityParallel = Plane.Parallel(sphere1.velocity, norm);
				var s2VelocityPerp = Plane.Perp(sphere1.velocity, norm);


                var mass = spheres.mass;
                var mass1 = sphere1.mass;


                var v1 =  (((mass - mass1) / (mass + mass1)) * s2VelocityParallel) + (((2 * mass1) / (mass + mass1) * s1VelocityParallel));
                var v2 = (((mass - mass1) / (mass + mass1)) * s2VelocityParallel) + (((2 * mass1) / (mass + mass1) * s1VelocityParallel));

                 var s1NewVelocity = v1 * spheres.coOfRestitution + s1VelocityPerp;
                var s2NewVelocity = v2 * sphere1.coOfRestitution + s2VelocityPerp;



            }


		}


	}
}
