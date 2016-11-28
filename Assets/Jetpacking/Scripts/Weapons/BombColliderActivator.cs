using UnityEngine;
using System.Collections;

public class BombColliderActivator : MonoBehaviour {

    public DelayedFuse fuse;
    public Collider collider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        collider.isTrigger = !fuse.armed;
	}
}
