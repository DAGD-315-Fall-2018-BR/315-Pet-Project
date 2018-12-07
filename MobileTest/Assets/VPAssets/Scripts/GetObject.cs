using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject: MonoBehaviour {

    public GameObject GameObject;
    public GameObject GameObject2;

    // Update is called once per frame
    public void getComb()
    {
        Instantiate(GameObject, transform);
    }
    public void getFood()
    {
        Instantiate(GameObject2, transform);
    }
}
