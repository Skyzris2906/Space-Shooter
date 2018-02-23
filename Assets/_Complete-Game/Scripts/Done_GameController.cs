using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	public Text winText;
	public Text lifesText;
	public Text bombsText;

	private bool win;
	private bool gameOver;
	private bool restart;
	public int score;

	void Start()
	{
		winText.text = "";
		UpdateLifes();
		UpdateBombs();
		win = false;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());

		Time.timeScale = 1;
	}

	void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

			if (win)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;

		if (score >= 1000)
		{
			Win();
		}

	}

	public void UpdateBombs()
	{
		bombsText.text = "Bombs: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Done_PlayerController>().bombs.ToString();
	}
	public void UpdateLifes()
	{
		lifesText.text = "Lifes: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Done_PlayerController>().lifes.ToString();
	}

	public void Win()
	{
		winText.text = "You Won!";
		win = true;
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}