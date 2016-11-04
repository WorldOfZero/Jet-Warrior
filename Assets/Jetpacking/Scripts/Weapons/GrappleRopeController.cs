using UnityEngine;
using System.Collections;

public class GrappleRopeController : MonoBehaviour {

    public Transform origin;
    public Transform target;
    public LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, target.position);
    }
}
