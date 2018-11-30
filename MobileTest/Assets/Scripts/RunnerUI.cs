using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class RunnerUI : MonoBehaviour {

	public Text GameOverText;
	public PlayerMovement player;

	private UnityAction listener;

	private void Awake()
	{
		listener = new UnityAction(UpdateHealth);
	}
	private void OnEnable()
	{
		EventManager.StartListening("playerDamage", listener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("playerDamage", listener);
	}

	private void UpdateHealth()
	{
		if(player.isDead == true)
		{
			GameOverText.enabled = true;
		}
	}
}
