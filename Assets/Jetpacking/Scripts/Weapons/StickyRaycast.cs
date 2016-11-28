using System;
using UnityEngine;
using System.Collections;

public class StickyRaycast : MonoBehaviour {

    public DelayedFuse fuse;

    public float range = 1;
    public Transform origin = null;

    public Component[] destroyOnConnect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(origin.position, origin.up);
        RaycastHit hit;
	    if (Physics.Raycast(ray, out hit, range))
	    {
            SendMessage("OnSpikeCollision", hit, SendMessageOptions.DontRequireReceiver);
	        if (fuse.armed)
	        {
                this.transform.parent = hit.transform;
                Array.ForEach(destroyOnConnect, (obj) => Destroy(obj));
	        }
	    }
	}

    void OnDrawGizmos()
    {
        if (origin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin.position, origin.position + origin.up * range);
        }
    }
}
