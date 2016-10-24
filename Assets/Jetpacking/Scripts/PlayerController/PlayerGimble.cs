using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerGravityController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerGimble : MonoBehaviour {

    private new Rigidbody rigidbody;
    private PlayerGravityController gravityController;

    public float maxDistanceLimit = 20;
    public float maxSpeedLimit = 5;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        gravityController = GetComponent<PlayerGravityController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gravityController.targetGravityEmitter != null)
	    {
	        if (rigidbody.velocity.magnitude < maxSpeedLimit &&
	            (gravityController.targetGravityEmitter.transform.position - rigidbody.worldCenterOfMass).magnitude <
	            maxDistanceLimit)
	        {
	            Quaternion targetRotation =
	                Quaternion.LookRotation(Vector3.Cross(this.transform.right, gravityController.targetUpVector),
	                    gravityController.targetUpVector);
	            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
	        }
	    }
	}
}
