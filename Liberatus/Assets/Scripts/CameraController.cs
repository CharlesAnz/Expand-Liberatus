using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController thePlayer; 

	private Vector3 lastPlayerPosition;
	private float distanceToMove;



		// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		lastPlayerPosition = thePlayer.transform.position;
		
		//Sets the player's position
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		//Finds player's position every frame, and follows player as it moves on the x-axis.

		distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
		
		transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
		
		lastPlayerPosition = thePlayer.transform.position;
	}
}
