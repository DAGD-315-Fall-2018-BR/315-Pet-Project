using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
	public Transform[] spawnPoints;
	public string code;

	public GameObject[] obstacleList;

	public float speed = 500;
	Rigidbody rb;
	private List<GameObject> obstacles = new List<GameObject>();
	// Use this for initialization
	void Start () {
		SpawnObstacles();
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, 0, 500);
	}


	void SpawnObstacles()
	{
		for(int i = obstacles.Count-1; i >= 0; i--)
		{
			Destroy(obstacles[i]);
		}
		obstacles.Clear();
		for (int i = 0; i < code.Length; i++)
		{
			if (code[i] == '0')
				continue;
			else
			{
				int c = int.Parse(code[i].ToString());
				if (float.IsNaN(c))
					continue;
				GameObject go = Instantiate(obstacleList[c - 1], spawnPoints[i].position, spawnPoints[i].rotation,transform);
				obstacles.Add(go);
			}
		}
	}

	string GenerateNewCode()
	{
		string tempCode = "";
		for (int i = 0; i < 12; i++)
		{
			char c = Random.Range(0, 4).ToString()[0];
			tempCode += c;
		}
		return tempCode;
	}

	void SetCode(string c)
	{
		code = c;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.A))
		{
			SetCode(GenerateNewCode());
			SpawnObstacles();
		}
	}

	private void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.fixedDeltaTime);
	}
}
