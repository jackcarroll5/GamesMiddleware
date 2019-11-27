using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankExploder : MonoBehaviour {
    [SerializeField]
    Transform[] tankParts;

    IEnumerator Start()
    {
        tankParts = new Transform[transform.childCount];
        for(int n = transform.childCount - 1; n >= 0; n--)
        {
            tankParts[n] = transform.GetChild(n);
            tankParts[n].SetParent(null, true);
            tankParts[n].GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-180,180), Random.Range(-180, 180), Random.Range(20, 180)));
            tankParts[n].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100,100), Random.Range(300, 600), Random.Range(-100, 100)));
        }

        yield return new WaitForSeconds(5);
        for(int n = 0; n < tankParts.Length; n++)
        {
            tankParts[n].GetComponent<MeshCollider>().enabled = false;
            Destroy(tankParts[n].gameObject, 5);
        }

        Destroy(gameObject);
    }
}
