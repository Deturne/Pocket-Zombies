using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        pointsText.text = PlayerController.points.ToString();

        if (Input.GetButtonDown("Switch"))
        {
            weapons[1].SetActive(false);
            weapons[0].SetActive(true);



        }


        if (Input.GetButtonDown("Switch 2"))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);


        }

        if (weapons[1].activeSelf)
        {
            {
                AmmoText.text = "Ar \n" + WeaponFireAr.currentLoadedAmmo + "/" + WeaponFireAr.currentSpareAmmo;
            }

        }

        if (weapons[0].activeSelf)
        {
            {
                AmmoText.text = "Pistol \n" + WeaponFire.currentLoadedAmmo + "/" + WeaponFire.currentSpareAmmo;
            }
          
        }
    }
}


    

