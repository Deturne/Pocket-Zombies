using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;
    private float maxHp = 150f;

    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        target.position = Camera.main.transform.position;
        currentHealth = maxHp;
        
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Zombie Got Hit: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.currentHealth = PlayerController.currentHealth - 5;
        }
    }
}
