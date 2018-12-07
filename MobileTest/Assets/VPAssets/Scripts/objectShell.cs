using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShell : MonoBehaviour
{

    public GameObject thisGameObject; 
    public bool isDead = false;
    private int deathCount = 0;
    public bool isFood = false;
    private bool canHit = true;

    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)//registers 1 or more fingers on the screen
        {
            Touch touch = Input.GetTouch(0);//gets only the first touch

            Vector3 touchPosition = Input.GetTouch(0).position;//creats a vector 3 based on the touch 0
            touchPosition.z = 3;//sets the z of the vector 3


            this.transform.position = Camera.main.ScreenToWorldPoint(touchPosition);//transforms the object to be where the touch position is
            this.transform.forward = Camera.main.transform.forward;//sets the forward vector to be the same as the camera. this is used for the raycast

            int layerMask = 1 << 9;//bit mask to only work on layer 9 or the brutus layer

            RaycastHit hit;//used for output
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))//does it hit
            {
                if (isFood) //checks to see if it is a food
                {
                    hit.transform.gameObject.GetComponent<PetObject>().hunger += 25;
                    isDead = true;
                }
                else
                {
                    if (canHit)
                    {
                        hit.transform.gameObject.GetComponent<PetObject>().cleanliness += 20;//gets the component in petObject of brutus when it hits and ups cleanliness
                        canHit = false;
                    }
                }
            }
            else
            {
                canHit = true;
            }

            if ( touch.phase == TouchPhase.Ended)
            {
                isDead = true;

            }
        
        }

        if (Input.touchCount == 0)
        {
            deathCount ++;


            if (deathCount > 30)
            {

                isDead = true;
            }
        }

        deathCome();
    }

    public void deathCome()
    {
        if (isDead)
        {
            Destroy(thisGameObject);
        }
    }


}
