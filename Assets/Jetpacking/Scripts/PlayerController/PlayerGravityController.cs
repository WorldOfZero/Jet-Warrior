using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGravityController : MonoBehaviour {

    private new Rigidbody rigidbody;
    private IEnumerable<GravityEmitter> gravityEmitters;

    public bool grounded = false;
    public GravityEmitter targetGravityEmitter;
    public Vector3 targetUpVector;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        gravityEmitters = FindObjectsOfType<GravityEmitter>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = float.MaxValue;
        GravityEmitter nearestEmitter = null;
	    foreach (var emitter in gravityEmitters)
	    {
            var emitterRange = (emitter.transform.position - rigidbody.worldCenterOfMass).magnitude;

            if (emitterRange < distance)
	        {
                nearestEmitter = emitter;
                distance = emitterRange;
	        }
	    }
        targetGravityEmitter = nearestEmitter;

	    if (nearestEmitter != null)
	    {
            Ray ray = new Ray(rigidbody.worldCenterOfMass, nearestEmitter.transform.position - rigidbody.worldCenterOfMass);
            RaycastHit hit;
	        if (Physics.Raycast(ray, out hit, float.MaxValue))
	        {
                targetUpVector = hit.normal;
                rigidbody.AddForce(-targetUpVector * 9.8f * Time.deltaTime * rigidbody.mass);
	        }
	    }
	}

    void OnCollisionEnter(Collision collider)
    {
        grounded = true;
        this.transform.parent = collider.transform;
    }

    void OnCollisionExit(Collision collider)
    {
        grounded = false;
        this.transform.parent = null;
    }
}
