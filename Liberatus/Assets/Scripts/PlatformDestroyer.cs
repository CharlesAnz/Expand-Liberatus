using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformDestroyer : MonoBehaviour {





	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerExit2D(Collider2D other) {
		// Sets everything that leaves the trigger not active in hierarchy
		other.gameObject.SetActive(false);
	}

}
