using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_DestroyByBombs : MonoBehaviour {

	private Done_GameController gameController;
	public GameObject explosion;
	public int scoreValue;
	// Use this for initialization
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift) && GameObject.FindGameObjectWithTag("Player").GetComponent<Done_PlayerController>().bombs != 0)
		{
			BombsExplosion();
		}
	}

	public void BombsExplosion()
	{
		Instantiate(explosion, transform.position, transform.rotation);
		gameController.AddScore(scoreValue);
		Destroy(gameObject);
	}
}
