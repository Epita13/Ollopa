using Godot;
using System;

public class SolarPanel : Building
{
    public static int nbSolarPanel;
    public int id;

    public static float energyMax = 200.0f;
    public float energy = 0;
    
    public static float sunPower = 1.5f;

    /* Signal pour les voyants */
    [Signal] public delegate void EnergyChange(float energy, float energyMax);

    private Timer timer;
    private Sprite stateSprite;

    public SolarPanel() : base (100)
    {
        id = nbSolarPanel;
        nbSolarPanel+=1;
    }

    public override void _EnterTree()
    {
        stateSprite = GetNode<Sprite>("state");
        timer = GetNode<Timer>("Timer");
        EmitSignal("EnergyChange", energy, energyMax);
        if (Environement.cycle == Environement.TimeState.DAY)
        {
            Color color = Color.Color8(66, 190, 40);
            stateSprite.Modulate = color;
            stateSprite.GetNode<Light2D>("Light").Color = color;
        }
        else
        {
            Color color = Color.Color8(255,0,0);
            stateSprite.Modulate = color;
            stateSprite.GetNode<Light2D>("Light").Color = color;
        }
    }


    public void _on_Timer_timeout()
    {
        if (isPlaced && Environement.cycle==Environement.TimeState.DAY)
        {
            AddEnergy(sunPower*timer.WaitTime);
            PrintEnergy();
            Color color = Color.Color8(66, 190, 40);
            stateSprite.Modulate = color;
            stateSprite.GetNode<Light2D>("Light").Color = color;
        }
        if (isPlaced && Environement.cycle==Environement.TimeState.NIGHT)
        {
            Color color = Color.Color8(255,0,0);
            stateSprite.Modulate = color;
            stateSprite.GetNode<Light2D>("Light").Color = color;
        }
        EmitSignal("EnergyChange", energy, energyMax);
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
