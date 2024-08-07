using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected Sprite image;
    [SerializeField] protected int cost;
    [SerializeField] protected String itemName;
    [SerializeField] protected String description;

    public Sprite returnSprite()
    {
        return image;
    }
    public String[] returnDescriptors()
    {
        String[] descriptors = new String[3];
        descriptors[0] = itemName;
        descriptors[1] = description;
        return descriptors;
    }

    public int returnCost()
    {
        return cost;
    }

}
