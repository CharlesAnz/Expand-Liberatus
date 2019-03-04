using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float moveSpeed;
	public float jumpForce;

	public float dashTime;
	private float dashTimeCounter;

	private Rigidbody2D myRigidbody;

	public float dashSpeed;


	public bool grounded;
	public LayerMask whatIsGround;

	private Collider2D myCollider;


	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();


		myCollider = GetComponent<Collider2D> ();

		dashTimeCounter = dashTime;



	}
	
	// Update is called once per frame
	void Update () {
	
		//Detects whether the player is on the ground
		grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);



		//Character moving forward at a constant speed
		myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
	

		//If shift is pressed, character will move forward quicker
		if (dashTimeCounter > 1.7) 
		{
			if (Input.GetKey (KeyCode.LeftShift)) 
				{
					myRigidbody.velocity = new Vector2 (moveSpeed * dashSpeed, myRigidbody.velocity.y);
					characterJump ();
				}
			dashTimeCounter -= Time.deltaTime;
			}
			
		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			dashTimeCounter = 0;
		}

		if (grounded)
		{
			dashTimeCounter = dashTime;
		}


		//If spacebar or mouse button is pushed then character will jump
		characterJump();
	
	
	
	
	}

	void characterJump()
	{
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) 
		{
			if(grounded){
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
			}
		}
	}
}
