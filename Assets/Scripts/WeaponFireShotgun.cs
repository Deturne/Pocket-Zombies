using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;


public class WeaponFireShotgun : MonoBehaviour
{


    public static int ammoCapacity = 8;
    public static int currentLoadedAmmo = 8;
    public static int currentSpareAmmo = 35;

    [SerializeField] bool canFire;
    [SerializeField] bool canReload;
    public GameObject bullets;

    [SerializeField] float bulletVelocity = 30;
    public float bulletLife = 2;
    int newAmmo;

    [SerializeField] AudioSource bulletSound;
    [SerializeField] AudioSource reload;
    [SerializeField] AudioSource shells;
    public static int weaponDamage = 450;

    [SerializeField] float fireRate = 0.3f;  // Fire rate for automatic fire (lower value = faster fire)
    [SerializeField] private Transform firePoint;
    private float nextFireTime = 0f;  // To track time between shots
    private bool isReloading = false;

    public int pellets = 10;         // Number of pellets per shot
    public float spreadAngle = 10f;
    [SerializeField] AudioSource cocking;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        canReload = false;
        canFire = true;
        ammoCapacity = 8;
        currentLoadedAmmo = ammoCapacity;
        currentSpareAmmo = 35;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && canFire && !isReloading)
        {
            if (currentLoadedAmmo > 0)
            {
                if (gameObject.activeSelf)
                {
                    Fire();
                    bulletSound.Play();
                    StartCoroutine(PlayCocking(cocking, 1));
                    nextFireTime = Time.time + fireRate;

                }
            }

            
        }

        if (PauseMenu.isPaused)
        {
            canFire = false;
        }
        else
        {
            canFire = true;
        }


        if (PlayerController.restarted)
        {
            ammoCapacity = 8;
            currentLoadedAmmo = ammoCapacity;
            currentSpareAmmo = 35;

        }

        if (Input.GetButtonDown("Reload") && currentSpareAmmo != 0)
        {
            Reload();
            bulletSound.Stop();
        }

        if (currentLoadedAmmo == 0 && currentSpareAmmo == 0)
        {
            canFire = false;
            canReload = false;
        }

        if (currentLoadedAmmo == 0 && canReload)
        {
            StartCoroutine(EmptyLoad());
        }

    }

    protected virtual void Reload()
    {
        canFire = false;
        if (currentLoadedAmmo < ammoCapacity && currentSpareAmmo >= 0)
        {
            
            if (currentSpareAmmo <= 0 && currentLoadedAmmo != 0)
            {
                if (currentLoadedAmmo >= 0)
                {
                    canReload = false;
                    
                }


            }
            canReload = true;
            
            
            StartCoroutine(ReloadShells(shells));
            
            animator.SetTrigger("Reload");
            StartCoroutine(ResetReload(animator));

            if (currentSpareAmmo <= 0)
            {
                currentLoadedAmmo = ammoCapacity - currentSpareAmmo;
                currentSpareAmmo = 0;
                canReload = false;
            }
        }
        



    }


    protected virtual void Fire()
    {

        if (canFire)
        {


            for (int i = 0; i < pellets; i++)
            {
                GameObject bullet = Instantiate(bullets, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(GetRandomSpread(Camera.main.transform.forward,spreadAngle,bulletVelocity), ForceMode.Impulse);
                StartCoroutine(DestroyBullet(bullet, bulletLife));

            }
            animator.SetTrigger("Shoot");
            currentLoadedAmmo--;


        }

        







        if (currentLoadedAmmo <= 0)
        {
            currentLoadedAmmo = 0;
            canFire = false;
        }

        if (currentLoadedAmmo >= 0)
        {
            canFire = true;
            canReload = true;
        }




        if (currentLoadedAmmo <= 0)
        {
            currentLoadedAmmo = 0;
            canFire = false;
        }

        if (currentLoadedAmmo > 0)
        {
            canFire = true;
        }
    }





    

    private IEnumerator DestroyBullet(GameObject bullet, float bulletLife)
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(bullet);
    }

    private Vector3 GetRandomSpread(Vector3 direction, float angle, float velocity)
    {
        Quaternion randomRotation = Quaternion.Euler(UnityEngine.Random.Range(-30, 30), UnityEngine.Random.Range(-30, 30), 0);
        return randomRotation * direction * velocity;
    }

    private IEnumerator PlayCocking(AudioSource cocking, float time)
    {
        yield return new WaitForSeconds(time);
        cocking.Play();
        animator.SetTrigger("Cock");
    }

    private IEnumerator ResetReload(Animator animator)
    {
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("Reload");
    }

    private IEnumerator ReloadShells(AudioSource shells)
    {
        
        
        while (currentLoadedAmmo <= ammoCapacity - 1)
        {
            canFire = false;
            isReloading = true;
            currentLoadedAmmo++;
            currentSpareAmmo--;
            shells.Play();
            yield return new WaitForSeconds(0.5f);
            canFire = true;
            isReloading = false;
        }
        StartCoroutine(PlayCocking(cocking, 0.5f));
        
    }

    private IEnumerator EmptyLoad()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(ReloadShells(shells));
    }


}
    
