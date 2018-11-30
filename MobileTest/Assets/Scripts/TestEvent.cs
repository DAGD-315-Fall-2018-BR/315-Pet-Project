using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestEvent : MonoBehaviour {
	private UnityAction someListener;

	private void Awake()
	{
		someListener = new UnityAction(SomeFunction);
	}
	private void OnEnable()
	{
		EventManager.StartListening("test", someListener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("test", someListener);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			EventManager.TriggerEvent("test");
		}
	}
	void SomeFunction()
	{
		Debug.Log("SomeFunction");
	}

}
