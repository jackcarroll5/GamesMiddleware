using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{




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


    }

   
	private class Collisions
	{

	}

	// Update is called once per frame
	private void Update()
	{








		for (int i = 0; i < allSpheres.Length; i++)
		{
		   BouncingScript spheres = allSpheres[i];

			foreach (Plane p in allPlanes)
			{
				if(spheres.isColliding(p))
				{
					Debug.Log("Collision occurs between Sphere " + spheres + "and Plane " + p);
                    Vector3 parallel_to_surface = p.parallellToSurface(spheres.velocity);
                    Vector3 perpendicular_to_surface = p.perpendicularToSurface(spheres.velocity);
                    spheres.transform.position -= spheres.velocity * Time.deltaTime;//Undoes the velocity

                    //transform.position -= distance * normal;



                    spheres.velocity = parallel_to_surface - perpendicular_to_surface;

                }

			}
            if (i < allSpheres.Length-1)
			for (int j = i + 1; j < allSpheres.Length; j++)
			{
			
				BouncingScript sphere1 = allSpheres[j];

                if (spheres.isColliding(sphere1))
				{
                
					Debug.Log("Collision occurs between Sphere " + spheres + "and Sphere " + sphere1);

                    Vector3 norm = spheres.getNormal(sphere1.transform.position);

                    //Debug.Log("Normal of spheres is: " + spheres.getNormal(otherPos));


                    var s1VelocityParallel = Plane.Parallel(spheres.velocity, norm);
                    var s1VelocityPerp = Plane.Perp(spheres.velocity, norm);

                    var s2VelocityParallel = Plane.Parallel(sphere1.velocity, norm);
                    var s2VelocityPerp = Plane.Perp(sphere1.velocity, norm);


                    var mass = spheres.mass;
                    var mass1 = sphere1.mass;


                    var v1 = (((mass - mass1) / (mass + mass1)) * s2VelocityParallel) + (((2 * mass1) / (mass + mass1) * s1VelocityParallel));
                    var v2 = (((mass - mass1) / (mass + mass1)) * s2VelocityParallel) + (((2 * mass1) / (mass + mass1) * s1VelocityParallel));

                   spheres.velocity = v1 * spheres.coOfRestitution + s1VelocityPerp;
                   sphere1.velocity = v2 * sphere1.coOfRestitution + s2VelocityPerp;
                }




            }


		}


	}
}
