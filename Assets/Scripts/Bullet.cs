using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnCollisionEnter(Collision collision)
    {
            if(gameObject.tag == "PistolBullet" || gameObject.tag == "ArBullet")
            {

                if (collision.transform.CompareTag("Zombie"))
                {
                    if(gameObject.tag == "PistolBullet")
                    {
                    collision.transform.GetComponent<EnemyAi>().TakeDamage(WeaponFire.weaponDamage);
                    PlayerController.points += 50;
                    }

                    if (gameObject.tag == "ArBullet")
                    {
                        collision.transform.GetComponent<EnemyAi>().TakeDamage(WeaponFireAr.weaponDamage);
                        PlayerController.points += 50;
                    }

            }
               
             
                
                Destroy(gameObject);
            }

        }
    }

