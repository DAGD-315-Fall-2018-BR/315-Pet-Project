using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
  //  public Transform trans;
    public Rigidbody rb;
    public Swipe swipeControls;
    
	private Animator anim;
    private bool doOnce = true; 
    
    private int lane = 2; //the lanes are numbered 1,2,3 from left to right
    
    private bool inAir = false;
    private bool isSliding = false;
    private bool isMoving = false;


    public float target = 0;
    float laneDis = 1.5f;

    public float smooth = 1.5f;
    public float speed = 1f; 
    public float jump = 1300f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
        swipeControls = GetComponent<Swipe>();
		//rb.useGravity = false; 
	}


	void Update()
    {
        //Controls, to be replaced at a later date with drag coontrols

        if ((Input.GetKeyDown("a") || swipeControls.SwipeLeft) && !isMoving && lane != 1)
        {

            isMoving = true;
            if(lane == 1)
            {
                target = 0;
            }
            else if (lane == 3)
            {
                lane = 2;
                target = -laneDis;
            }
            else if (lane == 2)
            {
                lane = 1;
                target = -laneDis;
            }
        }

        if ((Input.GetKeyDown("d")  || swipeControls.SwipeRight) && !isMoving && lane != 3)
        {
            if (lane == 3)
            {
                target =0;
            }
            else if (lane == 1)
            {
                lane = 2;
                target = laneDis;
            }
            else if (lane == 2)
            {
                lane = 3;
                target = laneDis;
            }
            isMoving = true;
        }

        if ((Input.GetKeyDown("w") || swipeControls.SwipeUp) && !inAir)
        {

            rb.AddForce(new Vector3(0, jump, 0));
            inAir = true;
			anim.SetBool("Jumping", true);
        }

        if ((Input.GetKey("s")  || swipeControls.SwipeDown) && !inAir && !isMoving && !isSliding)
        {
            //make player shorter     
            isSliding = true;
			anim.SetBool("Sliding", true);
        }

        //State Machine, moving the player to where they need to go and marking a boolean when they get there

        if (isSliding && (inAir || isMoving))
        {
            isSliding = false;
			anim.SetBool("Sliding", false);
		}
    }
	// Update is called once per frame
	void FixedUpdate () {

        if (isMoving)
        {
            rb.velocity = new Vector3(target * speed, rb.velocity.y, 0);
            if (((transform.position.x > -laneDis - 0.2 && transform.position.x < -laneDis +0.2) && lane == 1))
            {
                isMoving = false;
            }
            if (((transform.position.x > laneDis - 0.2 && transform.position.x < laneDis + 0.2) && lane == 3))
            {
                isMoving = false;
            }
            if ((transform.position.x < 0.2 && transform.position.x > -0.2) && lane == 2)
            {
                isMoving = false;
            }
            if(!isMoving)
            {
                float xPos = 0;
                if (lane == 1)
                    xPos = -laneDis;
                else if (lane == 2)
                    xPos = 0;
                else if (lane == 3)
                    xPos = laneDis;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                transform.position = new Vector3(xPos, transform.position.y, 0);
                
            }
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.transform.CompareTag("Ground"))
        {
            inAir = false;
			anim.SetBool("Jumping", false);
		}
    }
   
}
