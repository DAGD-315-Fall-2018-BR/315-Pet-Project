using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour {
	public List<Chunk> chunks;
	Chunk tempChunk;
	public static int speed = 6;
	// Use this for initialization
	public void SpawnChunks()
	{

	}

    public void LateUpdate()
    {
       if (chunks[0].transform.position.z < -1)
		{
			tempChunk = chunks[0];
			tempChunk.transform.position = chunks[chunks.Count - 1].transform.position + new Vector3(0, 0, 4.4f);
			chunks.RemoveAt(0);
			chunks.Add(tempChunk);
			tempChunk = null;
		} 
        
    }
	// Update is called once per frame
	void FixedUpdate ()
	{
		
	}
}
