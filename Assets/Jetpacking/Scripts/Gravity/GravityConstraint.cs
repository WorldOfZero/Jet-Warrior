using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class GravityConstraint : MonoBehaviour {

    public AnimationCurve gravityScalar;
    public AnimationCurve planeScalar;
    public Transform source;

    private new Rigidbody rigidbody;

    private Vector3 circleVelocity;
    private Vector3 planeVelocity;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        var difference = source.position - this.transform.position;
        var distance = Mathf.Abs(difference.magnitude);

        circleVelocity = difference.normalized * Time.deltaTime * gravityScalar.Evaluate(distance) * rigidbody.mass;
        rigidbody.AddForce(circleVelocity);

        var distanceY = this.transform.position.y;
        planeVelocity = Vector3.up * Time.deltaTime * planeScalar.Evaluate(Mathf.Abs(distanceY)) * rigidbody.mass * -Mathf.Sign(distanceY);
        rigidbody.AddForce(planeVelocity);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, circleVelocity);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, planeVelocity);
    }
}
