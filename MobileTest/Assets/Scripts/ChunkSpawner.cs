using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChunkSpawner : MonoBehaviour {
	public List<Chunk> chunks;
	public GameObject[] SpawnList;
	public string[] chunkCodes;
	Chunk tempChunk;
	public static int speed = 6;
	int baseSpeed = 6;
	float timer = 0;
	float timerBase = 5;
	bool boosted = false;
	public static ChunkSpawner cs = null;
	// Use this for initialization

	private UnityAction listener;

	private void Awake()
	{
		listener = new UnityAction(SpeedBoost);
		if(cs == null)
		{
			cs = this;
		}
		else
		{
			Destroy(this);
			Debug.Log("Only one chunk Spawner is allowed");

		}
	}
	private void OnEnable()
	{
		EventManager.StartListening("speedPowerup", listener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("speedPowerup", listener);
	}

	void SpeedBoost()
	{
		Debug.Log("Speed Powerup!");
		speed = 10;
		boosted = true;
		timer = 0;
	}

	void Update()
	{
		if(boosted)
		{
			timer += Time.deltaTime;
			if(timer > timerBase)
			{
				speed = baseSpeed;
				boosted = false;
				timer = 0;
			}
		}
	}
	public void SpawnChunks()
	{

	}

    public void LateUpdate()
    {
       if (chunks[0].transform.position.z < -4.4)
		{
			tempChunk = chunks[0];
			tempChunk.transform.position = chunks[chunks.Count - 1].transform.position + new Vector3(0, 0, 8.8f);
			chunks.RemoveAt(0);
			chunks.Add(tempChunk);
			tempChunk.GenerateNewCode();
			tempChunk = null;
		} 
        
    }
	// Update is called once per frame
	void FixedUpdate ()
	{
		
	}


}
