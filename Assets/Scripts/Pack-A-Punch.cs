using System.Collections;
using TMPro;
using UnityEngine;

public class PackAPunch : MonoBehaviour
{
    [SerializeField] int PackCost;
    [SerializeField] TextMeshProUGUI PackCostText;
    [SerializeField] GameObject weapon;

    bool inRange = false;
    [SerializeField] AudioSource ambiance;
    [SerializeField] GameObject weaponGet;

    public static bool packedAR;
    public static bool packedSG;
    public static bool packedPS;

    [SerializeField] TextMeshProUGUI verifyText;

    [SerializeField] private WallBuy wallBuyAR;
    [SerializeField] private WallBuy wallBuyShotgun;
    [SerializeField] private WallBuy wallBuyPistol;

    private WallBuy Wallweapons;
    GameObject Wallweapon;
    public int WallweaponCost;

    // Start is called before the first frame update
    void Start()
    {
        packedAR = false;
        packedSG = false;
        packedPS = false;
        weaponGet.SetActive(false);
        Wallweapons = FindObjectOfType<WallBuy>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && PlayerController.points >= PackCost)
        {
            if (inRange)
            {

                PlayerController.points -= PackCost;

                if (GameManager.currentWeapon.tag == "Ar")
                {

                    WeaponFireAr.weaponDamage *= 2;
                    packedAR = true;
                    Debug.Log(WeaponFireAr.weaponDamage);
                }




                // Notify WallBuy to update the AR cost
                //OnWeaponCostUpdated?.Invoke("Ar", 4000);


                if (GameManager.currentWeapon.tag == "Pistol")
                {
                    WeaponFire.weaponDamage *= 2;
                    packedPS = true;
                    Debug.Log(WeaponFire.weaponDamage);
                }


                // Notify WallBuy to update the Pistol cost
                //OnWeaponCostUpdated?.Invoke("Pistol", 4000);


                if (GameManager.currentWeapon.tag == "Shotgun")
                {
                    WeaponFireShotgun.weaponDamage *= 2;
                    packedSG = true;
                    Debug.Log(WeaponFireShotgun.weaponDamage);
                }

                weaponGet.SetActive(true);
                Instantiate(weaponGet);
                StartCoroutine(DisplayText(2));
            }


                // Notify WallBuy to update the Shotgun cost
                //OnWeaponCostUpdated?.Invoke("Shotgun", 4000);


                

            }
        }

    
  

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PackCostText.text = "Press E: " + PackCost + " To Upgrade";
            inRange = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PackCostText.text = "";
            inRange = false;
        }
    }

    private IEnumerator DisplayText(float time)
    {
        verifyText.text = "Weapon Upgraded!";
        yield return new WaitForSeconds(time);
        weaponGet.SetActive(false);
        verifyText.text = "";
    }
} 