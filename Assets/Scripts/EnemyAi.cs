using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;

    public Animator animator;
    public static float maxHp = 150f;
    public static float currentHealth;

    [SerializeField] GameObject impactFlesh;
    [SerializeField] AudioSource[] groans;
    int randomIndex;
    public bool killed;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        target = Camera.main.transform;
        target.position = Camera.main.transform.position;
        currentHealth = maxHp;
        int randomIndex = Random.Range(0, groans.Length);
        groans[randomIndex].Play();
        killed = false;
        


        // randomIndex = (int)Random.Range(0, GetComponent<AudioSource>().clip.length);
        //GetComponent<AudioSource>().time = GetComponent<AudioSource>().clip.length * randomIndex;

        // groans.Play();


    }
    public void TakeDamage(float damage)
    {
        if (killed) return;

        currentHealth -= damage;
        PlayerController.points += 50;
        Instantiate(impactFlesh);
        if (currentHealth <= 0)
        {
            Kill();
           

        }



    }
    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {

            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
            agent.SetDestination(targetPosition);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            PlayerController.currentHealth = PlayerController.currentHealth - 5;
            StartCoroutine(ResetHit(collision));
        }

        
    }


    IEnumerator StartAnimationWithDelay(float delay)
    {
        // Wait for the randomized delay
        yield return new WaitForSeconds(delay);

        // Start the animation (e.g., play the "Walk" animation)
        animator.Play("Zombie Walk", 0, Random.Range(0f, 1f)); // Random offset within the animation cycle
    }

    private IEnumerator ResetHit(Collision collision)
    {
        
        yield return new WaitForSeconds(1f);
        collision = null;




    }

    public void Kill()
    {
        if (killed) return;

        killed = true;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.zombiesKilled += 1;


        }
    }


}