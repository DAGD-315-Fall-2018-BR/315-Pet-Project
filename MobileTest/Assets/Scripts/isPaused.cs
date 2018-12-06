using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Keyboard;

public class scri : MonoBehaviour {

	// Use this for initialization
    
    private bool isPaused = false;
    
    UnityAction listener;
	private void Awake()
	{
		listener = new UnityAction(PauseGame);
	}
	private void OnEnable()
	{
		EventManager.StartListening("pauseGame", listener);
	}

	private void OnDisable()
	{
		EventManager.StopListening("pauseGame", listener);
        
	}
    
    
	void PauseGame () {
		isPaused = true
	}
	
	// Update is called once per frame
	void Update () {
        if(keyboard.ispressed(SPACE)){
            isPaused = false;
        }
		
	}
}
