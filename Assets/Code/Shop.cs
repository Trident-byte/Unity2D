using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject[] options;
    public static Shop shop;
    private float cost;
    [SerializeField] private TextMeshProUGUI resetText;


    public void Start()
    {
        shop = this;
        cost = 2;
    }

    public void Reset()
    {
        foreach (GameObject option in options)
        {
            ShopOption script = option.GetComponent<ShopOption>();
            script.SetItem();
        }
    }

    //Resets + takes money away from the reset
    public void ResetButton()
    {
        if (player.curPlayer.ReturnMoney() >= cost)
        {
            Reset();
            player.curPlayer.Spending(cost);
            cost *= 2;


            resetText.text = "Reset: " + cost + " coins";
        }
    }

    //Resets cost of reseting to original amount
    public void ResetCost()
    {
        cost = 2;
        resetText.text = "Reset: " + cost + " coins";
    }
}
