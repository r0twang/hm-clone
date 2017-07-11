using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public bool showInventory;

    private ItemDatabase database;
    private bool showTooltip;
    private string tooltip;

    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;

	// Use this for initialization
	void Start () {
        for (int i=0; i< (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if (draggingItem)
            {
                inventory[prevIndex] = draggedItem;
                draggingItem = false;
                draggedItem = null;
            }
            showInventory = !showInventory;
        }
    }

    void OnGUI()
    {
        //if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
        //    SaveInventory();
        //if (GUI.Button(new Rect(40, 450, 100, 40), "Load"))
        //    LoadInventory();
            tooltip = "";
        GUI.skin = skin;
        if(showInventory)
        {
            DrawInventory();
            if (showTooltip)
                GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("Tooltip"));
        }
        if(draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(x * 80, y * 80, 75, 75);
                GUI.Box(slotRect, "", skin.GetStyle("slot"));
                slots[i] = inventory[i];
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);
                    if (slotRect.Contains(Event.current.mousePosition))
                    {
                        tooltip = CreateTooltip(slots[i]);
                        showTooltip = true;
                        if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = slots[i];
                            inventory[i] = new Item();
                        }
                        if(e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
                        {
                            if (slots[i].itemType == Item.ItemType.Consumable)
                            {
                                UseConsumable(slots[i], i, true);
                            }
                        }
                    }

                } else
                {
                    if(slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                if(tooltip == "")
                {
                    showTooltip = false;
                }
                i++;
            }
        }
    }

    string CreateTooltip(Item item)
    {
        tooltip = "<color=#ffffff><b>" + item.itemName + "</b></color>\n\n" + item.itemDesc + "<b>\n\n\n\nMass: " + item.itemMass + "\nPower: " + item.itemPower + "</b>";
        return tooltip;
    }

    void RemoveItem(int ID)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == ID)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    void AddItem(int ID)
    {
        for(int i=0; i<inventory.Count; i++)
        {
            if(inventory[i].itemName == null)
            {
                for(int j = 0; j < database.items.Count; j++)
                {
                    if(database.items[j].itemID == ID)
                    {
                        inventory[i] = database.items[j];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int ID)
    {
        foreach (Item item in inventory)
        {
            if (item.itemID == ID) return true;
        }
        return false;
    }

    void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch(item.itemID)
        {
            case 2:
                {
                    //PlayerStats.increaseStat(1, 5, 30f);
                    print("Consumed: " + item.itemName);
                    break;
                }
        }
        if(deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    //void SaveInventory()
    //{
    //    for (int i=0; i < inventory.Count; i++)
    //    {
    //        PlayerPrefs.SetInt("Inventory " + i, inventory[i].itemID);
    //    }

    //}

    //void LoadInventory()
    //{
    //    for(int i = 0; i< inventory.Count; i++)
    //    {
    //        inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
    //    }
    //}
}
