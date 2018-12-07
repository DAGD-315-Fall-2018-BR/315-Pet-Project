using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour {
	public int happiness = 0;
	public GameObject heartParticle;
	Image image;
    public Color baseColor;
    public Color happyColor;

	private void Start()
	{
		image = GetComponent<Image>();
	}
	public void GetPet()
	{
		happiness++;
		GameObject go = Instantiate(heartParticle, transform);
		Destroy(go, 3);
		Debug.Log(happiness);

		if (happiness > 30)
		{
			image.color = happyColor;
		}
	}
    
    public void reset()
    {
        happiness = 0;
        image.color = baseColor;
    
    }

}

