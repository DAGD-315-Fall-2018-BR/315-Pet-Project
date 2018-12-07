using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetObject : MonoBehaviour {

    public GameObject heartParticle;
    public int happiness = 0;
    public int cleanliness = 0;
    public int hunger = 0;
    public Material d_Material;

    // Use this for initialization
    void Start() {
        d_Material = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update() {

        if (happiness >= 15)
        {
            d_Material.color = Color.yellow;
        }
        else if (cleanliness >= 200)
        {
            d_Material.color = Color.blue;
        }
        else if(hunger >= 100)
        {
            d_Material.color = Color.green;
        }
        else
        {
            d_Material.color = Color.white;
        }
    }  

    void OnMouseDown()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
            Debug.Log("Something Hit");
            if (raycastHit.collider.name == "Dog")
            {
                Debug.Log("Soccer Ball clicked");
                happiness++;
                GameObject go = Instantiate(heartParticle, transform);
                Destroy(go, 3);
            }
        }
    }

    public void reset()
    {
        happiness = 0;
        cleanliness = 0;
        hunger = 0;
    }
}
