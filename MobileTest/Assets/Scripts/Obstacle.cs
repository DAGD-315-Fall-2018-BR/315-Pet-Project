﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public enum ObstacleType
	{
		small,
		tall,
		airborne
	}
	public ObstacleType type;
}
