using UnityEngine;
using System.Collections;

public class ExplosionAnimator : MonoBehaviour {

    public float lifetime;
    private float aliveTime;

    [Header("Light")]
    public Light lightSource;
    public AnimationCurve lightCurve;

	// Use this for initialization
	void Start () {
        aliveTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        aliveTime += Time.deltaTime;

	    if (aliveTime > lifetime)
	    {
	        Destroy(this.gameObject);
	    }
	    else
	    {
            lightSource.intensity = lightCurve.Evaluate(aliveTime);
	    }
	}
}
