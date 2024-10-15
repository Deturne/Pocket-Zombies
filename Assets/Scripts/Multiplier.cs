using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("PistolBullet"))
        {
            PlayerController.points += 100;

        }

        if (collision.gameObject.CompareTag("ArBullet"))
        {


            PlayerController.points += 100;



        }
    }
}
