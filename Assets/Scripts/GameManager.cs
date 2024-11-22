using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI crosshair;

    [SerializeField] GameObject[] weapons;
    [SerializeField] TextMeshProUGUI KillText;

    public static int zombiesKilled = 0;
    public static int roundNumber = 1;
    public static int previousRound;
    public static int roundchangeNum = 0;

    [SerializeField] TextMeshProUGUI RoundText;
    [SerializeField] AudioSource switchsfx;
    public static bool switched;

    public static GameObject currentWeapon;


    // Start is called before the first frame update
    void Start()
    {
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        previousRound = roundNumber;
        currentWeapon = weapons[0];
        zombiesKilled = 0;




    }

    // Update is called once per frame
    void Update()
    {

        pointsText.text = PlayerController.points.ToString();
        SwitchWeapons();
       
        
        RoundText.text = roundNumber.ToString();
        KillText.text = "Zombies Killed: "+ zombiesKilled.ToString();

        if (previousRound > roundNumber)
        {
            EnemyAi.currentHealth = EnemyAi.currentHealth += 100;

            if(roundNumber > 10)
            {
                EnemyAi.currentHealth = EnemyAi.currentHealth * 1.1f;
            }
        }

        if (PlayerController.restarted)
        {
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);

        }


    }

    public GameObject[] weaponList()
    {
        return weapons;
    }

    

    void SwitchWeapons()
    {
        if (Input.GetButtonDown("Switch"))
        {
            if (WallBuy.ArBought)
            {
                weapons[1].SetActive(false);
                switchsfx.Play();
            }

            if (WallBuy.shotgunBought)
            {
                weapons[2].SetActive(false);
                switchsfx.Play();
            }
            

            weapons[0].SetActive(true);
            switched = true;
            currentWeapon = weapons[0];



        }


        if (Input.GetButtonDown("Switch 2"))
        {
            
            if (WallBuy.ArBought || MysteryBox.ArBought)
            {
                weapons[1].SetActive(true);
                weapons[2].SetActive(false);
                weapons[0].SetActive(false);
                switchsfx.Play();
                switched = true;
                currentWeapon = weapons[1];
            }
            else
            {
                weapons[1].SetActive(false);
            }

            if (WallBuy.shotgunBought || MysteryBox.shotgunBought )
            {
                weapons[2].SetActive(true);
                weapons[1].SetActive(false);
                weapons[0].SetActive(false);
                switchsfx.Play();
                switched = true;
                currentWeapon = weapons[2];
            }
            else
            {
                weapons[2].SetActive(false);
            }

            if (weapons[0].activeSelf)
            {
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
            }



        }


        if (weapons[2].activeSelf)
        {
            {
                AmmoText.text = "Shotgun\n" + WeaponFireShotgun.currentLoadedAmmo + "/" + WeaponFireShotgun.currentSpareAmmo;
                crosshair.text = "0";
            }

        }


        if (weapons[1].activeSelf)
        {
            {
                AmmoText.text = "Ar \n" + WeaponFireAr.currentLoadedAmmo + "/" + WeaponFireAr.currentSpareAmmo;
                crosshair.text = "+";
            }

        }

        if (weapons[0].activeSelf)
        {
            {
                AmmoText.text = "Pistol \n" + WeaponFire.currentLoadedAmmo + "/" + WeaponFire.currentSpareAmmo;
                crosshair.text = "+";
            }

        }
    }

    
   

}


    

