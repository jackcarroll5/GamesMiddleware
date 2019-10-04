using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    int randomBallNumbers;
	Plane[] allPlanes;
	BouncingScript[] allSpheres;
    public Transform spawnPosition;

    Color[] colours = new Color[6];



    // Start is called before the first frame update
    void Start()
	{
		allPlanes = FindObjectsOfType<Plane>();
		allSpheres = FindObjectsOfType<BouncingScript>();

        colours[0] = Color.yellow;
        colours[1] = Color.blue;
        colours[2] = Color.red;
        colours[3] = Color.green;
        colours[4] = Color.magenta;
        colours[5] = Color.black;
      

    }

    void spawningNewBalls()
    {
        randomBallNumbers = Random.Range(0,allSpheres.Length);

        Instantiate(allSpheres[randomBallNumbers],spawnPosition.position,spawnPosition.rotation);

    }

	private void OnValidate()
	{
		if(allSpheres == null)
		{
			allSpheres = FindObjectsOfType<BouncingScript>();
		}

    }


	// Update is called once per frame
	private void Update()
	{
        for (int i = 0; i < allSpheres.Length; i++)
		{
		   BouncingScript spheres = allSpheres[i];        

            foreach (Plane p in allPlanes)
            {
                float dist = 0;

                if (spheres.isColliding(p, ref dist))
				{
                    if (p.transform.tag == "Plane")
                    {
                        
                        Debug.Log("Collision occurs between Sphere " + spheres + "and Plane " + p);

                        spheres.GetComponent<Renderer>().material.color = colours[Random.Range(0, colours.Length)];

                        if (Input.GetMouseButtonDown(0))
                        {
                            spawningNewBalls();
                        }

                        Vector3 parallel_to_surface = p.parallellToSurface(spheres.velocity);
                    Vector3 perpendicular_to_surface = p.perpendicularToSurface(spheres.velocity);
                    spheres.transform.position -= dist * p.normal;

                    //transform.position -= distance * normal;

                    spheres.velocity = parallel_to_surface - perpendicular_to_surface;

                    Vector3 norm = spheres.getNormal(transform.position);
                    Debug.Log("Normal when colliding with plane for the " + spheres + " is: " + norm);

                    Vector3 poi = spheres.ptOfImpact(spheres);
                    Debug.Log("Point of Impact for the " + spheres + " when colliding with the plane is: " + poi);

                    BouncingScript aftermath = p.afterCollision(spheres, poi, norm, 0.6f);


                    Debug.Log("After Collision - Coefficient of Restitution for the " + spheres + " is: " + aftermath.coOfRestitution + "\nMass of Sphere: " + aftermath.mass + "\nRadius is: " + aftermath.radius);

                    if (Input.GetMouseButtonDown(0))
                    {
                        spawningNewBalls();
                    }
                }

              }

            }

            if (i < allSpheres.Length - 1)
            { 
                for (int j = i + 1; j < allSpheres.Length; j++)
                {

                    BouncingScript sphere1 = allSpheres[j];

                    if (spheres.isColliding(sphere1))
                    {

                        if (spheres.transform.tag == "Sphere")
                        {
                            if (Input.GetMouseButton(0))
                            {
                                spawningNewBalls();
                            }

                            Debug.Log("Ball collision occurs between Sphere " + spheres + "and Sphere " + sphere1);

                            Vector3 normal = spheres.normForCollision(sphere1);
                            Debug.Log("Normal for collision of sphere of " + spheres + "is: " + normal);

                            Vector3 norm = spheres.getNormal(sphere1.transform.position);
                            Debug.Log("Normal of sphere for the " + spheres + " is: " + norm);

                            Vector3 poi = spheres.ptOfImpact(sphere1);
                            Debug.Log("Point of Impact of the sphere for the " + spheres + " is: " + poi);


                            var s1VelocityParallel = Plane.Parallel(spheres.velocity, norm);

                            var s1VelocityPerp = Plane.Perp(spheres.velocity, norm);

                            var s2VelocityParallel = Plane.Parallel(sphere1.velocity, norm);
                            var s2VelocityPerp = Plane.Perp(sphere1.velocity, norm);


                            var mass = spheres.mass;
                            var mass1 = sphere1.mass;


                            var v1 = (((0 - mass1) / (0 + mass1)) * s2VelocityParallel) + (((2 * mass1) / (0 + mass1) * s1VelocityParallel));
                            var v2 = (((mass - mass1) / (mass + mass1)) * s2VelocityParallel) + (((2 * mass1) / (mass + mass1) * s1VelocityParallel));

                            spheres.velocity = v1 * spheres.coOfRestitution + s1VelocityPerp;
                            sphere1.velocity = v2 * sphere1.coOfRestitution + s2VelocityPerp;


                            spheres.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                        }
                    }

                }
            }


		}


	}
}
