using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] protected List<GameObject> items;

    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
    }

    public List<GameObject> RetrieveList()
    {
        return items;
    }
}
