using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObstacleModel))]
public class SplittingObstacleController : MonoBehaviour {

    public int minimumSplitNumber;
    public int maximumSplitNumber;
    public float scaleMultiplier;
    public float speedMultiplier;
    public float damageMultiplier;
    public float healthMultiplier;

    private ObstacleModel model;

	// Use this for initialization
	void Start () {
        model = GetComponent<ObstacleModel>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (model.health < 0)
	    {
            int number = Random.Range(minimumSplitNumber, maximumSplitNumber+1);
	        for (int i = 0; i < number; ++i)
	        {
                GameObject copy = (GameObject)Instantiate(this.gameObject, this.transform.position, Random.rotation);
                ObstacleModel copyModel = copy.GetComponent<ObstacleModel>();
                copyModel.baseHealth = model.baseHealth * healthMultiplier;
                copyModel.speed = model.speed * speedMultiplier;
                copyModel.damage = model.damage * damageMultiplier;
                copy.transform.localScale *= scaleMultiplier;
	        }
            Destroy(this.gameObject);
	    }
	}
}
