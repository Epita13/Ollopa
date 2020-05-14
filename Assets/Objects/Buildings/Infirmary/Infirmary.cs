using Godot;
using System;

public class Infirmary : Building
{
    public static int nbInfirmary;

    public override void _EnterTree()
    {
        id = nbInfirmary;
        nbInfirmary++;
    }
    
    public Infirmary() : base(100, 200)
    {
        
    }
}
