using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 5;
    float spawnTime = 0;
    [SerializeField] EnemyAi zombie;

    [SerializeField] int spawnMax = 24;
    [SerializeField] public int currentSpawned = 0;
    public GameObject[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 4; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

        spawnEnemy();
    }
    void spawnEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

        if (spawnPoints[randomIndex].activeSelf)
        {


            if (Time.time >= spawnTime && spawnMax > currentSpawned)
            {
                Instantiate(zombie, spawnPoints[randomIndex].transform.position, Quaternion.identity);
                currentSpawned++;
                spawnTime = Time.time + spawnRate;
            }

            if (Time.time >= spawnTime && currentSpawned == GameManager.zombiesKilled)
            {
                spawnMax += 24;
                Instantiate(zombie, spawnPoints[randomIndex].transform.position, Quaternion.identity);
                currentSpawned++;
                GameManager.roundNumber++;
                if (GameManager.previousRound > GameManager.roundNumber)
                {
                    EnemyAi.maxHp = EnemyAi.maxHp + 100;

                    if (GameManager.roundNumber > 10)
                    {
                        EnemyAi.currentHealth = EnemyAi.currentHealth * 1.1f;
                    }
                }
                    
                spawnTime = Time.time + spawnRate;
                Debug.Log(EnemyAi.currentHealth);
            }


            GameObject spawnPoint = spawnPoints[randomIndex];



        }
    }


    private IEnumerator SpawnDelay(float time)
    {
        yield return new WaitForSeconds(time);
        spawnEnemy();
        
    }

        public GameObject[] SpawnList()
        {
            return spawnPoints;
        }




}
