using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPop : MonoBehaviour {
	public GameObject[] buttons;
    public bool showingButtons = false;

	
	// Update is called once per frame
	public void toggle () {
        print("hello");
        if (showingButtons)
        {
			foreach(GameObject g in buttons)
			{
				g.SetActive(false);
			}
            showingButtons = false;
        }
        else if (!showingButtons)
        {
			foreach (GameObject g in buttons)
			{
				g.SetActive(true);
			}
            showingButtons = true;
        }
	}
}
