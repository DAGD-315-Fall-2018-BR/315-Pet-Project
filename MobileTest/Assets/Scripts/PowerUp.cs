using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public GameObject visual;

	public void Update()
	{
		visual.transform.Rotate(Vector3.up, 5,Space.World);
	}
	public enum PowerUpType
	{
		Collar,
		Helmat,
		other
	}
	public PowerUpType type;
}
