using UnityEngine;
using System.Collections;

public class SpawnOnDestruction : MonoBehaviour {

    public GameObject spawn;

	// Update is called once per frame
	void OnExplosion() {
        Instantiate(spawn, this.transform.position, this.transform.rotation);
	}
}
