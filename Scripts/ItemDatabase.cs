using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        items.Add(new Item("Newspaper", 0, "Popular daily newspaper", 0, 0.1, Item.ItemType.Stuff));
        items.Add(new Item("Gun", 1, "Generic gun", 5, 1.5, Item.ItemType.Weapon));
        items.Add(new Item("Beer", 2, "Typical beer from large company", 0, 0.5, Item.ItemType.Consumable));
        items.Add(new Item("Documents", 3, "Documents with various informations", 0, 0.1, Item.ItemType.Quest));
    }
}
