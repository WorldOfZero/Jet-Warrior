using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookController : MonoBehaviour {

    public GameObject projectile;
    public Rigidbody rootRigidbody;

    public JetpackController jetpackController;

    private bool grappleFired;
    private GameObject lastGrapple;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (!grappleFired)
	    {
	        if (jetpackController.grappleHook)
	        {
	            var grapple = Instantiate(projectile, jetpackController.grappleHookOrigin.position,
	                jetpackController.grappleHookOrigin.rotation);
	            lastGrapple = grapple;
	            var proj = grapple.GetComponent<GrapplingPhysicsController>();
                var rope = grapple.GetComponent<GrappleRopeController>();

	            if (proj != null)
	            {
	                proj.rootRigidbody = rootRigidbody;
	            }
	            else
	            {
	                Debug.LogWarning("No Projectile on grapple");
	            }
	            if (rope != null)
	            {
	                rope.points = new Transform[2];
	                rope.points[0] = grapple.transform;
	                rope.points[1] = jetpackController.grappleHookOrigin;
	            }
	            else
	            {
                    Debug.LogWarning("No Rope on grapple");
	            }
	        }
	        else
	        {
                if (lastGrapple != null)
                {
                    Destroy(lastGrapple);
                    lastGrapple = null;
                }
            }
	    }

	    grappleFired = jetpackController.grappleHook;

    }
}
