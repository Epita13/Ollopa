using Godot;
using System;

public class Printer3DInterface : BuildingInterface
{

    private Printer3D printer3D;
    

    public override void _Ready()
    {
        printer3D = (Printer3D)building;
    }
    
    
}
