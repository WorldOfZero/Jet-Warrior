using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    
    public int seed = (int)(Random.value * 1000000);
    public float chunkSize = 100;
    public int activationRadius = 5;
    public int deactivationRadius = 7;

    public WorldChunk[] availableChunkPrefabs;

    private WorldChunk[,] activeChunks;

	// Use this for initialization
	void Start () {
	    activeChunks = new WorldChunk[deactivationRadius * 2 + 1, deactivationRadius * 2 + 1];
        GenerateActiveChunks();
	}

    private void GenerateActiveChunks()
    {
        for (int x = deactivationRadius - activationRadius; x < deactivationRadius + activationRadius; ++x)
        {
            for (int y = deactivationRadius - activationRadius; y < deactivationRadius + activationRadius; ++y)
            {
                Random.InitState(x + y + seed);
                if (activeChunks[x, y] == null)
                {
                    var chunk = Instantiate(availableChunkPrefabs[0]);
                    chunk.transform.position = new Vector3(chunkSize * (x - deactivationRadius), 0, chunkSize * (y - deactivationRadius));
                    activeChunks[x, y] = chunk;
                    chunk.worldManager = this;
                    chunk.transform.parent = this.transform;
                }
            }
        }
    }

    // Update is called once per frame
	void Update () {
	    
	}
}
