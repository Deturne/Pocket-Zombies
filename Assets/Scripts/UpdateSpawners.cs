
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class UpdateSpawners : MonoBehaviour
{

    private Spawner spawns;
    [SerializeField] GameObject[] spawnsList;
    // Start is called before the first frame update
    void Start()
    {
        //spawns = FindObjectOfType<Spawner>();
        //spawnsList = spawns.SpawnList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < spawnsList.Length; i++)
                spawnsList[i].SetActive(true);
                
        }
            
        }

    }

