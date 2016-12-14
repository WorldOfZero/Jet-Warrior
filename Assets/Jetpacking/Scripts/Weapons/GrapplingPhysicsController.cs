using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingPhysicsController : MonoBehaviour {

    public Joint[] joints;
    public Rigidbody rootRigidbody;
    
	// Use this for initialization
    void ProjectileImpact()
    {
        foreach (var joint in joints)
        {
            joint.connectedBody = rootRigidbody;
        }
    }
}
