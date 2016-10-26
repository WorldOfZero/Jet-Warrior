using UnityEngine;
using System.Collections;

public class PlayerOrientationCaster : MonoBehaviour {

    public Material material;
    public JetpackController controller;
    public OrientationLockGimble orientation;

    public bool channeling = false;

    public LineRenderer lineRenderer;
    public GameObject plane;

	// Use this for initialization
	void Start () {
        CleanupObjects();
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (channeling)
	    {
	        Ray ray = new Ray(controller.pointerTransform.position, controller.pointerTransform.forward);
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit, float.MaxValue))
	        {
	            if (!controller.touchpadPress)
	            {
	                orientation.targetUpVector = hit.normal;
	                CleanupObjects();
	            }
	            else
	            {
	                material.SetVector("_WorldPos", new Vector4(hit.point.x, hit.point.y, hit.point.z, 0));

	                lineRenderer.enabled = true;
	                plane.SetActive(true);

	                plane.transform.position = hit.point + hit.normal*0.01f;
	                plane.transform.rotation = Quaternion.LookRotation(hit.normal);

	                lineRenderer.useWorldSpace = true;
	                lineRenderer.SetPosition(0, controller.pointerTransform.position);
	                lineRenderer.SetPosition(1, hit.point);
	            }
	        }
	        else
	        {
	            CleanupObjects();
                lineRenderer.enabled = true;
                lineRenderer.useWorldSpace = true;
                lineRenderer.SetPosition(0, controller.pointerTransform.position);
                lineRenderer.SetPosition(1, controller.pointerTransform.position + controller.pointerTransform.forward * 100);
            }
	    }
	    else
	    {
            CleanupObjects();
	    }

	    channeling = controller.touchpadPress;
	}

    private void CleanupObjects()
    {
        lineRenderer.enabled = false;
        plane.SetActive(false);
    }
}
