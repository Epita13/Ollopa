using Godot;
using System;

public class PetrolGenerator : Building
{
    public static int nbPetrolGenerator;
    private static float power = 1f;
    private static readonly float oilProduc = 1f;
    public float oilMAX = 500f;
    public float oil = 0;
    public float togive = 0;
    private bool on = false;
    private static float power2wake = 2 * power;
    private static float giveSpeed = 2.5f;

    public override void _EnterTree()
    {
        id = nbPetrolGenerator;
        nbPetrolGenerator++;
    }

    public void _on_Timer_timeout()
    {
        if (togive >= giveSpeed)
        {
            //Player.inventoryLiquids.Add(Liquid.Type.Oil, giveSpeed);
            togive -= giveSpeed;
            oil -= giveSpeed;
        }
        else if(togive > 0)
        {
            //Player.inventoryLiquids.Add(Liquid.Type.Oil, togive);
            oil -= togive;
            togive = 0;
        }

        if (on)
        {
            RemoveEnergy(power * timer.WaitTime);
            oil += oilProduc * timer.WaitTime;   
        }

        if (oil > oilMAX)
            oil = oilMAX;

        on = (energy >= power2wake && !on || energy > 0 && on) && oil < oilMAX;
    }

    public PetrolGenerator() : base(100, 200)
    {
        
    }
}
