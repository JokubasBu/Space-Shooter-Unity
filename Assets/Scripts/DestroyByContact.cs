using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	public bool Boss;

	private int lives;
    private GameController gameController;
	private PlayerController playerController;


    void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        if (playerObject == null)
        {
            Debug.Log("Cannot find 'PlayerObject' script");
        }

        if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' script");
		}

        if (Boss)
        {
           lives = 5;
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Boss")) {
			// Ignore Boundary
			return;
		}

        if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
        if (Boss && (lives > 1) && other.CompareTag("Bolt"))
        {
            Destroy(other.gameObject);
            lives = lives - 1;
            return;
        }


		if (other.CompareTag("Bolt"))
		{
			Destroy(other.gameObject); // Bolt or Player
		}

        if (gameObject.CompareTag("Reward1") && other.CompareTag("Player"))
        {
            gameController.AddScore(500);
            gameController.LoseLive(-1);
        }
        if (gameObject.CompareTag("Reward2") && other.CompareTag("Player"))
        {
            gameController.LoseLive(-2);
        }
        if (gameObject.CompareTag("Reward3") && other.CompareTag("Player"))
        {
            playerController.StartFireRateBoost();
            gameController.LoseLive(-1);
        }
        if (gameObject.CompareTag("Reward4") && other.CompareTag("Player"))
        {
            playerController.StartSpeedBoost();
            gameController.LoseLive(-1);
        }

        if (other.CompareTag("Player"))
        {

            if (gameController.LoseLive(1) == 0)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
                Destroy(other.gameObject);
            }

        }
        else
        {
            gameController.AddScore(scoreValue); // Only add to the score when not hitting the Player!
        }

        Destroy(gameObject); // Asteroid
	}
}
