using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireAr : MonoBehaviour
{
    public static int ammoCapacity = 30;
    public static int currentLoadedAmmo = 30;
    public static int currentSpareAmmo = 120;

    [SerializeField] bool canFire;
    [SerializeField] bool canReload;
    public GameObject bullets;

    [SerializeField] float bulletVelocity = 30;
    public float bulletLife = 2;
    int newAmmo;

    [SerializeField] AudioSource bulletSound;
    [SerializeField] AudioSource reload;
    public static int weaponDamage = 45;
    [SerializeField] float fireRate = 0.3f;  // Fire rate for automatic fire (lower value = faster fire)

    [SerializeField] private Transform firePoint;

    private float nextFireTime = 0f;  // To track time between shots


    // Start is called before the first frame update
    void Start()
    {
        canReload = false;
        canFire = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) {
            if(currentLoadedAmmo > 0)
            {
                Fire();
                bulletSound.Play();
                nextFireTime = Time.time + fireRate;

            }
        }

        if (Input.GetButtonDown("Reload") && currentSpareAmmo != 0)
        {
            Reload();
            reload.Play();
            bulletSound.Stop();
        }

        if (currentLoadedAmmo == 0 && currentSpareAmmo == 0)
        {
            canFire = false;
            canReload = false;
        }
    }

    protected virtual void Reload()
    {
        if (currentLoadedAmmo < ammoCapacity && currentSpareAmmo >= 0)
        {
            if (currentSpareAmmo <= 0 && currentLoadedAmmo != 0)
            {
                if (currentLoadedAmmo >= 0)
                {
                    canReload = false;
                    canFire = false;
                }


            }
            canReload = true;
            canFire = true;
            newAmmo = ammoCapacity - currentLoadedAmmo;
            currentSpareAmmo -= newAmmo;
            currentLoadedAmmo = ammoCapacity;

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
            GameObject bullet = Instantiate(bullets, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward.normalized * bulletVelocity, ForceMode.Impulse);

            StartCoroutine(DestroyBullet(bullet, bulletLife));
            currentLoadedAmmo--;
        }

        if (currentLoadedAmmo <= 0)
        {
            currentLoadedAmmo = 0;
            canFire = false;
        }
    }

    private IEnumerator DestroyBullet(GameObject bullet, float bulletLife)
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(bullet);
    }
}
