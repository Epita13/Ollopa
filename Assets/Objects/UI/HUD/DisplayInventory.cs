using Godot;
using System;
using System.Collections.Generic;

public class DisplayInventory : ItemList
{
    public override void _Ready()
    {
        Visible = false;
    }
    
    public override void _Process(float delta)
    {
       if (Visible)
           RectPosition = PlayerMouvements.instance.Position;
    }

    private void Display()
    {
        PlayerState.state = PlayerState.State.Menu;
        Clear();
        for (int i = 0; i < Item.nbItems; i++)
            AddItem(Convert.ToString(Player.inventoryItems.GetItemCount((Item.Type)i)), Item.textures[i] , true);
        Visible = !Visible;
    }
}
