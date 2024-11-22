using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Bullet : MonoBehaviour
{

    [SerializeField] AudioSource impact;
                     
    
    public static bool shotgunImpact;

    public void Start()
    {
        shotgunImpact = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
           

                if (collision.transform.CompareTag("Zombie"))
                {
                    if(gameObject.tag == "PistolBullet")
                    {
                        collision.transform.GetComponent<EnemyAi>().TakeDamage(WeaponFire.weaponDamage);
                        Destroy(gameObject);
                    }   

                    if (gameObject.tag == "ArBullet")
                    {
                        collision.transform.GetComponent<EnemyAi>().TakeDamage(WeaponFireAr.weaponDamage);
                        Destroy(gameObject);


                    }

                    if (gameObject.tag == "Pellet")
                    {
                        
                        collision.transform.GetComponent<EnemyAi>().TakeDamage(WeaponFireShotgun.weaponDamage);

                        Destroy(gameObject);

                    }
                    
                    
                    Debug.Log("Sound Played");

                    impact.Play();

                }
                else
                {
                    
                    Debug.Log("Sound Played");
                    impact.Play();
                    //Destroy(gameObject);
                }






            

    }
    
}


