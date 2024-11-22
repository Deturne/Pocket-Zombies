using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallBuy : MonoBehaviour
{
    [SerializeField] int weaponCost;
    [SerializeField] TextMeshProUGUI weaponCostText;
    [SerializeField] GameObject weapon;

    bool inRange = false;
    private GameManager gameManager;
    GameObject[] weapons;


    public static bool ArBought;
    public static bool shotgunBought;
    public static bool pistolBought;



    // Start is called before the first frame update
    void Start()
    {
        ArBought = false;
        inRange = false;
        shotgunBought = false;
        pistolBought = false;
        gameManager = FindObjectOfType<GameManager>();

        weapons = gameManager.weaponList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && PlayerController.points >= weaponCost)
        {
            if (inRange)
            {
                PlayerController.points -= weaponCost;
                if (gameObject.tag == "Ar")
                {
                    ArBought = true;
                    shotgunBought = false;
                    pistolBought = false;
                    weapons[1].SetActive(true);
                    weapons[2].SetActive(false);
                    weapons[0].SetActive(false);
                }

                if (ArBought)
                {
                    WeaponFireAr.currentLoadedAmmo = WeaponFireAr.ammoCapacity;
                    WeaponFireAr.currentSpareAmmo = 120;
                }

                if (gameObject.tag == "Shotgun")
                {
                    shotgunBought = true;
                    ArBought = false;
                    pistolBought = false;
                    weapons[2].SetActive(true);
                    weapons[1].SetActive(false);
                    weapons[0].SetActive(false);
                }

                if (shotgunBought)
                {
                    WeaponFireShotgun.currentLoadedAmmo = WeaponFireShotgun.ammoCapacity;
                    WeaponFireShotgun.currentSpareAmmo = 35;
                }

                if (gameObject.tag == "Pistol")
                {
                    shotgunBought = false;
                    ArBought = false;
                    pistolBought = true;
                    weapons[0].SetActive(true);
                    weapons[1].SetActive(false);
                    weapons[2].SetActive(false);
                }

                if (pistolBought)
                {
                    WeaponFire.currentLoadedAmmo = WeaponFire.ammoCapacity;
                    WeaponFire.currentSpareAmmo = 75;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weaponCostText.text = "Press E: " + weaponCost + " To Buy" + " " + gameObject.tag;
            inRange = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weaponCostText.text = "";
            inRange = false;
        }
    }

    public void UpdateWeaponCost(string weaponTag, int newCost)
    {
        if (gameObject.tag == weaponTag)
        {
            weaponCost = newCost;

            if (inRange)
            {
                weaponCostText.text = "Press E: " + weaponCost + " To Buy" + " " + gameObject.tag;
            }

            // Update the cost only if the tag matches this weapon's tag
            Debug.Log($"Updating cost for tag: {weaponTag}, current object tag: {gameObject.tag}");

        }
    }
}
