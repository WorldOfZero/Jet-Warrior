using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class JetpackController : MonoBehaviour {

    public float triggerPosition;
    public Vector2 wheelPosition;
    public float wheelRotation;
    public bool holdBomb;
    public bool touchpadPress;
    public bool grappleHook;
    public Transform thrusterTransform;
    public Transform pointerTransform;
    public Transform grappleHookOrigin;

    SteamVR_TrackedObject trackedObject;

	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        triggerPosition = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
        wheelPosition = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        wheelRotation = Mathf.Atan2(wheelPosition.y, wheelPosition.x);
        holdBomb = device.GetPress(EVRButtonId.k_EButton_Grip);
        touchpadPress = device.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad);
        grappleHook = device.GetPress(EVRButtonId.k_EButton_ApplicationMenu);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(thrusterTransform.position, thrusterTransform.up);
        if (pointerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pointerTransform.position, pointerTransform.up);
        }
    }
}
