using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerGravityController))]
public class OrientationLockGimble : MonoBehaviour {
    
    private PlayerGravityController gravityController;

    public Vector3 targetUpVector;

	// Use this for initialization
	void Start () {
        gravityController = GetComponent<PlayerGravityController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gravityController.grounded)
	    {
            targetUpVector = gravityController.targetUpVector;
        }
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(this.transform.right, targetUpVector), targetUpVector);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
    }
}
