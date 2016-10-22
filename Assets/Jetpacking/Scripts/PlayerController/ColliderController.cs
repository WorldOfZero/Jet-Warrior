using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class ColliderController : MonoBehaviour {

    public Transform tracked;
    private new CapsuleCollider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent< CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
        collider.center = new Vector3(tracked.localPosition.x, collider.center.y, tracked.localPosition.z);
	}
}
