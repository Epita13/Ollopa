using Godot;
using System;
using System.Collections.Generic;

public class UI_PlayerInventory : Node
{
    public static UI_PlayerInventory instance;
    public static UI_PlayerInventory GetInstance() => instance;

    private ItemList itemList;
    
    
    public override void _Ready()
    {
        instance = this;
        itemList = GetNode<ItemList>("list");
        itemList.Visible = false;
    }
    
    public static void Display()
    {
        GetInstance().itemList.Clear();
        for (int i = 0; i < Item.nbItems; i++)
            GetInstance().itemList.AddItem(Convert.ToString(Player.inventoryItems.GetItemCount((Item.Type)i)), Item.textures[i] , true);
        GetInstance().itemList.Visible = true;
    }
    
    public static void Hide()
    {
        GetInstance().itemList.Visible = false;
    }
}
