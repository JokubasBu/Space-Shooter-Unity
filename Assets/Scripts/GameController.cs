using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] asteroids;
	public GameObject[] rewardRocks;
    public GameObject boss;
    public Vector3 spawnValues;
	public int asteroidCount;
    public int rewardRarity;
    public float startWait;
	public float spawnWait;
	public float waveWait;
	public Text scoreText;
    public Text livesText;
    public Text restartText;
	public Text gameOverText;
    public int lives;

	private int score;
	private int bossWave;
	private static int highScore;
	private bool gameOver;
	private bool restart;

	void Start()
	{
        int highScore = PlayerPrefs.GetInt("highScore");

        if (highScore == null)
		{
			highScore = 0;
		}
		bossWave = 1;
		score = 0;
		UpdateScore();
		UpdateLives();

        gameOver = false;
		gameOverText.text = "";

		restart = false;
		restartText.text = "";

		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown(KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);

		while (true)
		{
				if (bossWave * 1000 < score)
				{
					Instantiate(boss, new Vector3(0, 0, 13), Quaternion.identity);
					bossWave += 1;
				}

				for (int i = 0; i < asteroidCount; i++)
				{

					Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;

					GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
					Instantiate(asteroid, spawnPosition, spawnRotation);


					yield return new WaitForSeconds(spawnWait);
				}

				if (Random.Range(0, 100) < rewardRarity)
				{
					Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
				Instantiate(rewardRocks[Random.Range(0,3)], spawnPosition, spawnRotation);
				}

				yield return new WaitForSeconds(waveWait);

				if (gameOver)
				{
					restart = true;
					restartText.text = "Press 'R' to Restart";
					break;
				}
			}
    }

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString();
	}

	void UpdateLives()
    {
        livesText.text = "Lives:  " + lives.ToString();
    }

    public int LoseLive(int amount)
    {
		lives -= amount;
        UpdateLives();
		return lives;
    }


    public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	public void GameOver()
	{
		if (score > highScore)
		{
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            gameOver = true;
            gameOverText.text = "GAME OVER" + System.Environment.NewLine + "NEW HIGH SCORE: " + PlayerPrefs.GetInt("highScore");
        } else
		{
            gameOver = true;
            gameOverText.text = "GAME OVER" + System.Environment.NewLine + "YOUR SCORE: " + score.ToString() + System.Environment.NewLine +
			"HIGH SCORE: " + PlayerPrefs.GetInt("highScore");
        }
	}
}
