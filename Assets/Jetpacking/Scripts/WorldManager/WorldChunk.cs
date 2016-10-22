using UnityEngine;
using System.Collections;

public class WorldChunk : MonoBehaviour {

    public WorldManager worldManager;
    private int seed;

    public float gridSize = 20;
    public GameObject[] objects;

	// Use this for initialization
	void Start () {
        seed = worldManager.seed + Mathf.RoundToInt(this.transform.position.x) + Mathf.RoundToInt(this.transform.position.z);
        GenerateWorld();
	}

    private void GenerateWorld()
    {
        Random.InitState((int)seed);
        for (float x = 0; x < worldManager.chunkSize; x += gridSize)
        {
            for (float z = 0; z < worldManager.chunkSize; z += gridSize)
            {
                GameObject newInstance = (GameObject)Instantiate(objects[Random.Range(0, objects.Length)], this.transform.position, this.transform.rotation);
                newInstance.transform.parent = this.transform;
                newInstance.transform.localPosition = new Vector3(x * gridSize, 0, z * gridSize);
            }
        }
    }
}
