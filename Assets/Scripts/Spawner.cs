using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnRate = 5;
    float spawnTime = 0;
    [SerializeField] GameObject zombie;
    int spawnMax = 24;
    [SerializeField] int currentSpawned = 0;
    [SerializeField] List<GameObject> spawnList;

    // Start is called before the first frame update
    void Start()
    {
        
        //if (currentSpawned < spawnMax)
        //{
            
            
        //    InvokeRepeating("spawnEnemy", 1, spawnRate);
        //    spawnList.Add(zombie);
            
        //}
    }

    // Update is called once per frame
    void Update()
    {


        //InvokeRepeating("spawnEnemy",1,spawnRate);
        if (Time.time >= spawnTime && spawnMax > spawnList.Count) 
        {
            spawnEnemy();
            spawnTime = Time.time + spawnRate;
        }
       
            
        

    }
    void spawnEnemy()
    {

        
        
            Instantiate(zombie, transform.position, Quaternion.identity);
            spawnList.Add(zombie);
            
        


    }

    
}
