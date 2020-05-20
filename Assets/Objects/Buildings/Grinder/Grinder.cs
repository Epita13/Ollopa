using Godot;
using System;

public class Grinder : Building
{
    public static int nbGrinder;
    public static float power = 1f;
    public float o2MAX = 500f;
    public float o2 = 0;
    public float togive = 0;
    private static float giveSpeed = 2f;
    
    
    public Grinder() : base (200, 200.0f)
    {
    }

    public override void _EnterTree()
    {
        id = nbGrinder;
        nbGrinder += 1;
    }

    public void Grind(Item.Type type)
    {
        Player.inventoryItems.Remove(type, Item.ToComposite[type]);
        Player.inventoryItems.Add(Item.Type.Composite, 1);
    }
    
    
}
