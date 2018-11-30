using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Points : MonoBehaviour {

	private float score = 0.0f;
	private bool hasPointMult = false;
	private float timer = 0.0f;
	private float timerBase = 5.0f;
	private int pointMult = 1;
	private int basePointMult = 1;

	UnityAction listener;
	private void Awake()
	{
		listener = new UnityAction(PointMultPowerup);
	}
	private void OnEnable()
	{
		EventManager.StartListening("pointMultPowerup", listener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("pointMultPowerup", listener);
	}




	void PointMultPowerup()
	{
		Debug.Log("MORE POINTS!");
		pointMult = 2;
		hasPointMult = true;
		timer = 0;
	}

	void Update()
	{
		score += Time.deltaTime * pointMult;
		//Debug.Log(Mathf.RoundToInt(score));

		if (hasPointMult)
		{
			timer += Time.deltaTime;
			if (timer > timerBase)
			{
				Debug.Log("Powerup Done");
				pointMult = basePointMult;
				hasPointMult = false;
				timer = 0;
			}
		}
	}
}
