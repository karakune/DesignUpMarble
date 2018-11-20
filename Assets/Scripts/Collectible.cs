using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	private GameController  gameController;
	public int pointsValue;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			gameController.AddPoints(pointsValue);
			Destroy(gameObject);
		}
	}
}
