using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RotationForceAdder : MonoBehaviour {

    public float maxRotationForce = 100;

    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddTorque(Random.insideUnitSphere * maxRotationForce);
        Destroy(this);
	}
}
