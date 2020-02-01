using Godot;
using System;
using System.Collections.Generic;



public class ValuesPanel : Panel
{

    List<Label> txts = new List<Label>();

    public override void _Ready()
    {
        foreach(var node in GetNode("VBox").GetChildren())
        {
            txts.Add((Label)node);
        }
    }

    public override void _Process(float delta)
    {
      txts[0].SetText("energy : "+ Player.energy);
      txts[1].SetText("Oxygene : " + World.size );
    }
}
