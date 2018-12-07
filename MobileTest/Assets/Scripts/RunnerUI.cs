using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class RunnerUI : MonoBehaviour {

	public Text GameOverText;
	public Text HealthUI;
	public Text ScoreText;
	public GameObject homeButton;
	public PlayerMovement player;
	public Points points;

	private UnityAction listener;

	private void Awake()
	{
		listener = new UnityAction(UpdateHealth);
	}

	private void Start()
	{
		UpdateHealth();
	}
	private void OnEnable()
	{
		EventManager.StartListening("playerDamage", listener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("playerDamage", listener);
	}

	private void Update()
	{
		ScoreText.text = "Score: " + points.GetScore();
	}
	private void UpdateHealth()
	{
		if(player.isDead == true)
		{
			GameOverText.enabled = true;
			HealthUI.enabled = false;
			ScoreText.enabled = false;
			homeButton.SetActive(true);
		}
		else
		{
			HealthUI.text = "Health: " + player.health;
		}
	}
}
