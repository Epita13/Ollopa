using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Policy;

public class Storage : Building
{

	/* Signal pour les voyants */
	[Signal] public delegate void EnergyChange(float energy, float energyMax);
	[Signal] public delegate void OxygeneChange(float energy, float energyMax);

	private Node2D LED;
	private Sprite oxygeneSprite;
	
	//energy
	public float energy = 0;
	public float maxEnergy  = 1000f;
	//oxygéne
	public float oxygene = 0;
	public float maxOxygene = 1000f;
	 
	
	//ID de chaque batiment
	public int nbStorage = 0;
	public int id;

	//Initialisation
    public Storage() : base (150)
    {
        this.id = nbStorage;
		nbStorage += 1;
    }
	
	 public override void _Ready()
    {
	    EmitSignal("OxygeneChange", energy, maxEnergy);
	    EmitSignal("EnergyChange", energy, maxEnergy);
	    LED = GetNode<Node2D>("LED");
	    oxygeneSprite = GetNode<Sprite>("Image/oxygene");
	    RefreshLED();
	    RefreshOxygene();
    }
	 

	 private void RefreshOxygene()
	 {
		 Color color = oxygeneSprite.Modulate;
		 int alpha = (int)(oxygene * 255 / maxOxygene);
		 color.a8 = alpha;
		 oxygeneSprite.Modulate = color;
	 }
	 
	 private void RefreshLED()
	 {
		 if (energy >= maxEnergy && oxygene >= maxOxygene)
		 {
			 Color color = Color.Color8(255,0,0);
			 LED.GetNode<Sprite>("led").Modulate = color;
			 LED.GetNode<Light2D>("Light").Color = color;
		 }
		 else
		 {
			 Color color = Color.Color8(66, 190, 40);
			 LED.GetNode<Sprite>("led").Modulate = color;
			 LED.GetNode<Light2D>("Light").Color = color;
		 }
	 }
	 
	public void AddEnergy(float amount)
	{
		energy += amount;
		if(energy >= maxEnergy)
			energy = maxEnergy;
		RefreshLED();
		EmitSignal("EnergyChange", energy, maxEnergy);
	}
	
	public bool RemoveEnergy(float amount)
	{
			bool verif = false;
			if (energy - amount >= 0)
			{
				energy -= amount;
				verif = true;
			}
			EmitSignal("EnergyChange", energy, maxEnergy);
			RefreshLED();
			return verif;
	}
	public void AddOxygene(float amount)
	{
		oxygene += amount;
		if(oxygene >= maxEnergy)
			oxygene = maxEnergy;
		EmitSignal("OxygeneChange", oxygene, maxOxygene);
		RefreshLED();
		RefreshOxygene();
	}

	public bool RemoveOxygene(float amount)
	{
		bool verif = false;
		if (oxygene - amount >= 0)
		{
			oxygene -= amount;
			verif = true;
		}
		EmitSignal("OxygeneChange", oxygene, maxOxygene);
		RefreshLED();
		RefreshOxygene();
		return verif;
	}
}
