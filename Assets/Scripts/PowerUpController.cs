using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerUpController : MonoBehaviour
{
    public GameObject[] powerup;
    public Vector3 spawnValues;
    public int powerupCount = 1;
 
    void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    
    void Update()
    {
        
    }

    IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(15);
        while (true)
        {
            for (int i = 0; i < powerupCount; i++)
            {
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = transform.rotation;

                if (Random.Range(0f, 1f) < 0.5f)
                {
                    Instantiate(powerup[Random.Range(0,1)], spawnPosition, spawnRotation);
                }
                else
                {
                    Instantiate(powerup[2], spawnPosition, spawnRotation);
                }
            }
         yield return new WaitForSeconds(20);

        }
    }
}
