using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Thruster : MonoBehaviour {

    public JetpackController jetpackController;
    public float force = 10;
    private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody.AddForce(jetpackController.triggerPosition * jetpackController.thrusterTransform.up * force * Time.deltaTime);
	}
}
