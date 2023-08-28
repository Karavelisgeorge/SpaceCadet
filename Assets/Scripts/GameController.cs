using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] Boss;
    
    public Vector3 spawnValues;
    public int hazardCount = 3;
    public int bossCount = 2;
    
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public float spawnWaitBoss;
    public float startWaitBoss;
    public float waveWaitBoss;

    public Text restartText;
    public Text gameOverText;
    public Text quitButtonText;

    private bool gameOver;
    private bool restart;

    public float bossScore = 1500f;

    

    void Start()
    {
        gameOver = false;
        restart = false;

        restartText.text = "";
        gameOverText.text = "";
        quitButtonText.text = "";

        StartCoroutine (SpawnWaves());
        StartCoroutine (Hazardsincrease());
        StartCoroutine (SpawnBoss());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ScoreScript.scoreValue = 0;
                SceneManager.LoadScene("Gameplay");
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
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = transform.rotation;


                if(Random.Range(0f, 1f) < 0.8f) {

                    Instantiate(hazards[Random.Range(0, 2)], spawnPosition, spawnRotation);

                } else if (Random.Range(0f, 1f) < 0.7f) {

                    Instantiate(hazards[Random.Range(2, 4)], spawnPosition, spawnRotation);

                } else if (Random.Range(0f, 1f) < 0.5f) {

                    Instantiate(hazards[Random.Range(4, 6)], spawnPosition, spawnRotation);

                } else if (Random.Range(0f, 1f) < 0.4f) {

                    Instantiate(hazards[6], spawnPosition, spawnRotation);
                }

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                quitButtonText.text = "Quit";
                restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }
        }     
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(startWaitBoss);
        while (true)
        {
            for (int i = 0; i < bossCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = transform.rotation;

                if (ScoreScript.scoreValue >= bossScore)
                {
                    Instantiate(Boss[0], spawnPosition, spawnRotation);
                    bossScore += 1500;
                }

                yield return new WaitForSeconds(spawnWaitBoss);
            }
            yield return new WaitForSeconds(waveWaitBoss);
        }
        
    }


    IEnumerator Hazardsincrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(25);

            HazardsIncrement();
        }
    }

    void HazardsIncrement()
    {
        hazardCount++;
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("Highscore") < ScoreScript.scoreValue)
        {
            PlayerPrefs.SetInt("Highscore", ScoreScript.scoreValue);
        }  
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
