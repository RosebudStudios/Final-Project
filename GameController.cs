using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnwait;
    public float startwait;
    public float wavewait;

    public Text ScoreText;
    public Text GameOverText;
    private int score;
    private bool gameOver;
    private bool restart;
    public static bool win;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;

    private void Start() { 
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOver = false;
        win = false;
        GameOverText.text = "";

        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (restart)
        {
            if (Input.GetKey("q"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startwait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnwait);
            }
            yield return new WaitForSeconds(wavewait);

            if (gameOver)
            {
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
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            YouWin();
        }
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over! \n Press Q to Restart";
        gameOver = true;
        restart = true;

        if (musicSource.clip != musicClipTwo)
        {
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    public void YouWin()
    {
        GameOverText.text = "You Win! \n Made By Connor Voorhees";
        restart = true;
        gameOver = true;
        win = true;

        if (musicSource.clip != musicClipThree)
        {
            musicSource.Stop();
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }

        else
        {
            return;
        }
    }
}
