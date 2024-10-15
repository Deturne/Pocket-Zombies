using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] int ammoCapacity;
    [SerializeField] int currentLoadedAmmo;
    [SerializeField] int currentSpareAmmo;
    //[SerializeField] bool canFire;
    protected bool canReload;
    public GameObject bullets;

    [SerializeField] private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        canReload = false;
        //canFire = true;
        currentLoadedAmmo = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Fire();
            Instantiate(bullets);
        }
    }

    protected virtual void Reload()
    {
        if (canReload = false && currentLoadedAmmo < ammoCapacity && currentSpareAmmo != 0)
        {
            canReload = true;
            //canFire = false;
            for (int i = currentLoadedAmmo; currentLoadedAmmo >= ammoCapacity; i++)
            {
                currentSpareAmmo--;
                canReload = false;

            }

        }
        if (currentSpareAmmo == 0)
        {
            canReload = false;
        }
    }


    //protected virtual void Fire()
    //{
    //    RaycastHit hit;
    //    if (canFire)
    //    {
    //        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
    //        {
    //            Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
    //            if (hit.transform.CompareTag("Zombie"))
    //            {
    //                hit.transform.GetComponent<EnemyAi>().TakeDamage(1);
    //            }
    //        }
    //        currentLoadedAmmo--;
    //    }
    //}
}
