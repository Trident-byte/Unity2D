using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class ShopOption : MonoBehaviour
{
    private GameObject item;
    [SerializeField] private Button option;
    [SerializeField] private TextMeshProUGUI itemName;
    private int cost;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private ItemList itemList;
    private int randObject;
    private List<GameObject> items;

    // Start is called before the first frame update
    void Start()
    {
        SetItem();
    }

    public void SetItem()
    {
        items = itemList.RetrieveList();
        randObject = UnityEngine.Random.Range(0, items.Count);
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
        bool bought = player.curPlayer.GetPowerUP(item, cost);
        if (bought)
        {
            Item itemScript = item.GetComponent<Item>();
            if (itemScript.onetime == true)
            {
                itemList.RemoveItem(item);
                Debug.Log("removing");
            }
            LevelManager.manager.newRound();
        }

    }
}
