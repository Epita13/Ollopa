using Godot;
using System;

public class SolarPanel : Building
{
    public static int nbSolarPanel;
    public int id;

    public static float energyMax = 200.0f;
    public float energy = 0;

    public static bool isDay = true;
    public static float sunPower = 1.0f;

    public SolarPanel() : base (100)
    {
        id = nbSolarPanel;
        nbSolarPanel+=1;
    }

    public override void _Process(float delta)
    {
        if (isPlaced && isDay)
        {
            AddEnergy(sunPower*delta);
            PrintEnergy();
        }
        
    }

    private void AddEnergy(float amount)
    {
        energy += amount;
        if (energy>energyMax)
            energy = energyMax;
    }

    private void RemoveEnergy(float amount)
    {
        energy -= amount;
        if (energy<0)
            energy = 0;
    }

    public void PrintEnergy()
    {
            GD.Print("Le panneau solaire "+id+" est a "+energy+"/"+energyMax+" d'energie.");
    }

}
