using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}


	void OnTriggerEnter2D(Collider2D other) {
		// Reloads the level when player falls to the trigger
		SceneManager.LoadScene("Scenes/Scene1");
		//Application.LoadLevel("Scene1");
	}
	
}
