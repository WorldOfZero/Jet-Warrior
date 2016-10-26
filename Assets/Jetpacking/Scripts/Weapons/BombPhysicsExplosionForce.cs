using UnityEngine;
using System.Collections;
using System.Linq;

public class BombPhysicsExplosionForce : MonoBehaviour {

    public float force = 100;
    public float radius = 10;
    public int explosionDensity = 16;

    public void OnExplosion()
    {
        Debug.Log("Exploding Bomb with " + explosionDensity + " rays");
        for (int i = 0; i < explosionDensity; ++i)
        {
            Ray ray = new Ray(this.transform.position, Random.onUnitSphere);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, radius))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddExplosionForce(force, this.transform.position, radius);
                }
            }
        }
    }
}
