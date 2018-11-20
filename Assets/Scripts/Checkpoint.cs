using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	private GameController  gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController").GetComponent<GameController>();		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			gameController.SetRespawnPoint(gameObject.transform.position);
		}
	}
}
