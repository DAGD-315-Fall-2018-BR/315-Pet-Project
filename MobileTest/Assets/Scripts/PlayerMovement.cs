using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
  //  public Transform trans;
    public Rigidbody rb;
    public Swipe swipeControls;
	public int health = 5;
	public bool isDead = false;
	private Animator anim;
    private bool doOnce = true; 
    
    private int lane = 2; //the lanes are numbered 1,2,3 from left to right
    
    private bool inAir = false;
    private bool isSliding = false;
    private bool isMoving = false;


    public float target = 0;
    public float laneDis = 2 * 0.68747f;
    public float laneThreshold = 0.2f;

    public float smooth = 1.5f;
    public float speed = 1f; 
    public float jump = 1300f;
	public float fallMod = 5;
	public Vector3 vel;
	public float gravity;

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

			//rb.AddForce(new Vector3(0, jump, 0));
			vel.y = jump;
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
			//rb.velocity = new Vector3(target * speed, rb.velocity.y, 0);
			vel = new Vector3(target * speed, vel.y, 0);
            if (((transform.position.x > -laneDis - laneThreshold && transform.position.x < -laneDis +laneThreshold) && lane == 1))
            {
                isMoving = false;
            }
            if (((transform.position.x > laneDis - laneThreshold && transform.position.x < laneDis + laneThreshold)  && lane == 3))
            {
                isMoving = false;
            }
            if ((transform.position.x < laneThreshold && transform.position.x > -laneThreshold) && lane == 2)
            {
                isMoving = false;
            }
			if (transform.position.x > laneDis || transform.position.x < -laneDis)
			{
				isMoving = false;
			}
			//transform.position += vel * Time.fixedDeltaTime;

			
            if(!isMoving)
            {
                float xPos = 0;
                if (lane == 1)
                    xPos = -laneDis;
                else if (lane == 2)
                    xPos = 0;
                else if (lane == 3)
                    xPos = laneDis;
				//rb.velocity = new Vector3(0, rb.velocity.y, 0);
				vel = new Vector3(0, vel.y, 0);
                transform.position = new Vector3(xPos, transform.position.y, 5);   
            }
        }
		transform.position += vel * Time.fixedDeltaTime;
		if (transform.position.y < 0)
		{
			vel.y = 0;
			transform.position = new Vector3(transform.position.x, 0, 5);
			inAir = false;
			anim.SetBool("Jumping", false);
		}
		else
		{
			
			vel.y -= gravity * Time.fixedDeltaTime;
			if(vel.y < 0)
			{
				vel.y -= fallMod* gravity * Time.fixedDeltaTime;
			}
		}
	}

	void TakeDamage()
	{
		health--;
		if(health <= 0)
		{
			isDead = true;
		}
		EventManager.TriggerEvent("playerDamage");
	}
	private void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.CompareTag("Obstacle"))
		{
			Obstacle o = c.gameObject.GetComponent<Obstacle>();
			if (o.type == Obstacle.ObstacleType.airborne)
			{
				if (!isSliding)
				{
					TakeDamage();
				}
			}
			else if (o.type == Obstacle.ObstacleType.small)
			{
				if (!inAir)
				{
					TakeDamage();
					EventManager.TriggerEvent("speedPowerup");
				}
			}
			else if (o.type == Obstacle.ObstacleType.tall)
			{	
				TakeDamage();
				EventManager.TriggerEvent("pointMultPowerup");
				
			}
		}
	}
}
