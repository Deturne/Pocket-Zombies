using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    [SerializeField] int boxCost;
    [SerializeField] TextMeshProUGUI boxCostText;
    GameObject currentweapon;
    [SerializeField] AudioSource weaponGet;
    GameObject mainweapon;

    private GameManager gameManager;
    GameObject[] weapons;
    public static bool ArBought;

    public static bool shotgunBought;
    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        ArBought = false;
        shotgunBought = false;
        gameManager = FindObjectOfType<GameManager>();

        // Get weapons from GameManager
        weapons = gameManager.weaponList();

    }

    void Update()
    {
        if (Input.GetKeyDown("e") && inRange && PlayerController.points >= boxCost)
        {
            PlayerController.points -= boxCost;

            


            int randomIndex = Random.Range(1, weapons.Length);
            //GameObject selectedWeapon = weapons[randomIndex];
            GameObject weaponObject = weapons[randomIndex].GameObject();
            // Play weapon obtained sound
            weaponGet.Play();

           
            if (weaponObject.tag == "Ar")
            {
                ArBought = true;
                shotgunBought = false;
                weapons[1].SetActive(true);
                weapons[2].SetActive (false);
                weapons[0].SetActive(false);

            }

            if (ArBought)
            {
                WeaponFireAr.currentLoadedAmmo = WeaponFireAr.ammoCapacity;
                WeaponFireAr.currentSpareAmmo = 120;


            }

            if (weaponObject.tag == "Shotgun")
            {
                shotgunBought = true;
                ArBought = false;

                weapons[2].SetActive(true);
                weapons[1].SetActive(false);
                weapons[0].SetActive(false);

            }


            if (shotgunBought)
            {
                WeaponFireShotgun.currentLoadedAmmo = WeaponFireShotgun.ammoCapacity;
                WeaponFireShotgun.currentSpareAmmo = 35;


            }

        }

        if (weapons[0].activeSelf)
        {
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
        }
    }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                boxCostText.text = "Press E: " + boxCost + " For a Random Weapon";
                inRange = true;
            }
            else
            {
                boxCostText.text = " ";

            }
        }

        void OnTriggerExit(Collider collision)
        {
            boxCostText.text = " ";
            inRange = false;
        }
    }

    

