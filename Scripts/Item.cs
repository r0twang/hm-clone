using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item /*: MonoBehaviour*/
{
    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public double itemPower;
    public double itemMass;
    public ItemType itemType;

    public enum ItemType
    {
        Weapon,
        Stuff,
        Consumable,
        Quest
    }

    public Item(string name, int ID, string desc, double power, double mass, ItemType type)
    {
        itemName = name;
        itemID = ID;
        itemDesc = desc;
        itemPower = power;
        itemMass = mass;
        itemType = type;
        itemIcon = Resources.Load<Texture2D>("ItemIcons/" + name);
    }

    public Item()
    {

    }
}
