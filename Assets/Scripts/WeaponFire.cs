using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public static int ammoCapacity = 15;
    public static int currentLoadedAmmo = ammoCapacity;
    public static int currentSpareAmmo = 75;

    [SerializeField] bool canFire;
    [SerializeField] bool canReload;
    public GameObject bullets;

    [SerializeField] float bulletVelocity = 30;
    public float bulletLife = 2;
    int newAmmo;

    [SerializeField] AudioSource bulletSound;
    public static int weaponDamage = 20;
    public static string weaponType = "Pistol";

    [SerializeField] private Transform firePoint;
    [SerializeField] Animator shootanim;
    [SerializeField] AudioSource reload;

    [SerializeField] public Transform gunDefault;
    public bool Reloading;

    // Start is called before the first frame update
    void Start()
    {
        canReload = false;
        canFire = true;
        shootanim = GetComponent<Animator>();
        ammoCapacity = 15;
        currentLoadedAmmo = ammoCapacity;
        currentSpareAmmo = 75;
        

}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Fire();
            bulletSound.Play();
            shootanim.SetTrigger("Shoot");

        }

        if (Input.GetButtonDown("Reload") && currentSpareAmmo != 0)
        {
            Reload();
            gunDefault.position = new Vector3(1479, 305.1f, 309.536f);


        }

        if (currentLoadedAmmo == 0 && canReload)
        {
            StartCoroutine(EmptyLoad());
        }


        if (PauseMenu.isPaused)
        {
            canFire = false;
            canReload = false;
            return; // Exit early to prevent further input handling
        }
        else
        {
            canFire = currentLoadedAmmo > 0;
            canReload = currentSpareAmmo > 0;
        }

        if (currentLoadedAmmo == 0 && currentSpareAmmo == 0)
        {
            canFire = false;
            canReload = false;
        }

        if(currentSpareAmmo == 0)
        {
            canReload = false;
        }
    }

    protected virtual void Reload()
    {
        if (currentLoadedAmmo < ammoCapacity && currentSpareAmmo >= 0)
        {
            if (currentSpareAmmo <= 0 && currentLoadedAmmo != 0)
            {
                //if (currentLoadedAmmo >= 0)
                //{
                //    canReload = false;
                //    canFire = false;
                //}
                
                
            }

            
            canReload = true;
            canFire = true;
            newAmmo = ammoCapacity - currentLoadedAmmo;
            currentSpareAmmo -= newAmmo;
            currentLoadedAmmo = ammoCapacity;
            shootanim.SetTrigger("Reload");
            StartCoroutine(ResetReload(shootanim));
            reload.Play();

            if (currentSpareAmmo <= 0)
            {
                currentSpareAmmo = 0;
                canReload = false;
            }

            if(currentSpareAmmo < ammoCapacity)
            {
                currentLoadedAmmo = ammoCapacity-currentSpareAmmo;
            }

        }

        

    }


    protected virtual void Fire()
    {

        if (canFire)
        {
            GameObject bullet = Instantiate(bullets, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward.normalized * bulletVelocity, ForceMode.Impulse);

            StartCoroutine(DestroyBullet(bullet, bulletLife));
            currentLoadedAmmo--;
        }

        if(currentLoadedAmmo <= 0)
        {
            currentLoadedAmmo = 0;
            canFire = false;
        }
        else
        {
            canFire = true;
            canReload = true;
        }

        
    }

    private IEnumerator DestroyBullet(GameObject bullet, float bulletLife)
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(bullet);
    }

    private IEnumerator ResetReload(Animator animator) { 
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger("Reload");
    }

    private IEnumerator EmptyLoad()
    {
        yield return new WaitForSeconds(1f);
        Reload();
    }
}
