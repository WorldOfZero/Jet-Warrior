using UnityEngine;
using System.Collections;

public class FlightStickController : MonoBehaviour {

    public Transform player;
    public JetpackController controller;
    public OrientationLockGimble orientationLockGimble;

    public float pitchStrength;
    public float rollStrength;

    public float pitchVelocity;
    public float rollVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        pitchVelocity = controller.wheelPosition.y * pitchStrength;
        rollVelocity = controller.wheelPosition.x * rollStrength;

        orientationLockGimble.targetUpVector = Quaternion.AngleAxis(pitchVelocity, player.transform.right) * orientationLockGimble.targetUpVector;
        orientationLockGimble.targetUpVector = Quaternion.AngleAxis(-rollVelocity, player.transform.forward) * orientationLockGimble.targetUpVector;
    }
}
