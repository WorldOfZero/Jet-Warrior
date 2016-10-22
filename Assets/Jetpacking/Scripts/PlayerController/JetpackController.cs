using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class JetpackController : MonoBehaviour {

    public float triggerPosition;
    public Transform thrusterTransform;

    SteamVR_TrackedObject trackedObject;

	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        triggerPosition = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(thrusterTransform.position, thrusterTransform.up);
    }
}
