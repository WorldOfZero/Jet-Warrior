using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObstacleModel))]
public class BasicObstacleController : MonoBehaviour {

    private ObstacleModel model;

	// Use this for initialization
	void Start () {
        model = GetComponent<ObstacleModel>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += model.speed * this.transform.forward * Time.deltaTime;
	}
}
