using Godot;
using System;
using System.Collections.Generic;

public class Printer3D : Building
{
    /* Signal pour les voyants */
    
    
    //ID de chaque batiment
    public static int nbPrinter3D = 0;

    private Sprite bar;
    
    
    
    public Printer3D() : base (150, 100.0f)
    {
    }
    
    
    public override void _EnterTree()
    {
        this.id = nbPrinter3D;
        nbPrinter3D += 1;

        bar = GetNode<Sprite>("Image/bar");
        SetBar(0.50f);
    }

    private void SetBar(float p)
    {
        float a = ((1-p) * 25.5f) - 22.0f;
        bar.Position = new Vector2(bar.Position.x, a);
    }
   
}
