﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour {
	public List<Chunk> chunks;
	Chunk tempChunk;
	// Use this for initialization
	public void SpawnChunks()
	{

	}
	
	// Update is called once per frame
	void FixedUpdate ()
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
}