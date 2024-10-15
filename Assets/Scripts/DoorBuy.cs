using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorBuy : MonoBehaviour
{
    [SerializeField] int doorCost;
    [SerializeField] TextMeshProUGUI doorCostText;
    [SerializeField] GameObject door;
    bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && PlayerController.points >= doorCost)
        {
            if (inRange)
            {
                Destroy(door);
                doorCostText.text = " ";
                PlayerController.points -= doorCost;
            }
            
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorCostText.text = "Press E: " + doorCost + " To Buy";
            inRange = true;
        }
        else
        {
            doorCostText.text = " ";
            
        }
    }

    void OnTriggerExit(Collider collision)
    {
        doorCostText.text = " ";
        inRange = false;
    }
}
