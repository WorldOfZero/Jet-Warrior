using UnityEngine;
using System.Collections;

public class DelayedFuse : MonoBehaviour {

    public bool armed = false;
    public float timer = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (armed)
	    {
	        timer -= Time.deltaTime;
	        if (timer < 0)
	        {
                Destroy(this.gameObject);
	        }
	    }
	}
}
