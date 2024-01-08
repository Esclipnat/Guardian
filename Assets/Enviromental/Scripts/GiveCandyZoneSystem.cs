using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveCandyZoneSystem : MonoBehaviour
{
    //public int candy;
    public PlayerController playerC;
    public TextMesh candyText;
    public AudioSource SFXThanks;
    private GameManagerController gmInst;

    private void Start()
    {
        gmInst = GameManagerController.instance;
    }
    private void Update()
    {
        //candyText.text = playerC.candiesInHand.ToString();
        candyText.text = gmInst.candy.ToString();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Child") && gmInst.candy > 0)
        {
            other.GetComponent<ChildController>().hasCandy = true;
            GameManagerController.instance.sumPoints(1);
            gmInst.candy--;
            SFXThanks.Play();
            //GetComponent<TMPro.TextMeshProUGUI>().SetText("dasd");

        }

        if (other.CompareTag("Player"))
        {
            /*
            PlayerController playerC = other.GetComponent<PlayerController>();
            if (playerC.candiesInHand > 0)
            {
                candy += playerC.candiesInHand;
                playerC.candiesInHand = 0;
            }
            */
        }

    }
}