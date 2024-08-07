using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOption : MonoBehaviour
{
    [SerializeField] protected GameObject[] items;
    private GameObject item;
    [SerializeField] private Button option;
    [SerializeField] private TextMeshProUGUI itemName;
    private int cost;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        SetItem();
    }

    public void SetItem()
    {
        int randObject = UnityEngine.Random.Range(0, items.Length);
        item = items[randObject];
        Item itemScript = item.GetComponent<Item>();
        String[] descriptors = itemScript.returnDescriptors();
        itemName.text = descriptors[0];
        description.text = descriptors[1];
        cost = itemScript.returnCost();
        price.text = "Cost: " + cost + " coins";
        option.GetComponent<Image>().sprite = itemScript.returnSprite();
    }

    public void GetPowerUP()
    {
        bool bought = false;
        bought = player.curPlayer.GetPowerUP(item, cost);
        if (bought)
        {
            LevelManager.manager.newRound();
        }
    }
}
