using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public JetpackController jetpackController;
    public float deadZone = 0.25f;
    public float[] degreeGates;

    public int index = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 trackpad = jetpackController.wheelPosition;
        float length = trackpad.magnitude;
	    if (length > deadZone)
	    {
            float degrees = jetpackController.wheelRotation * Mathf.Rad2Deg;
            degrees += 360;
            degrees %= 360;
            index = 0;
	        for (int i = 0; i < degreeGates.Length; ++i)
	        {
	            if (degrees > degreeGates[i])
	            {
	                index = i;
	            }
	            else
	            {
                    break;
	            }
	        }
	    }
	}
}
