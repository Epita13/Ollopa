using Godot;
using System;
using System.Collections.Generic;

public class Item
{
    public static int nbItems = 6;
    public enum Type
    {
        Composite,
        Wood,
        Stone,
        Dirt,
        Sonar,
        Ospirit,
    }

    public static Dictionary<int, Texture> textures = new Dictionary<int, Texture>
    {
        {(int)Type.Composite, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Composite.png")},
        {(int)Type.Wood, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Wood.png")},
        {(int)Type.Stone, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Stone.png")},
        {(int)Type.Dirt, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Dirt.png")},
        {(int)Type.Sonar, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Sonar.png")},
        {(int)Type.Ospirit, GD.Load<Texture>("res://Assets/Ressources/Imgs/Items/Ospirit.png")}
    };
    
    public static List<Type> item2heal = new List<Type>
    {
        Type.Sonar,
        Type.Wood,
        Type.Ospirit,
    };

    public static Dictionary<Type, float> healingPower = new Dictionary<Type, float>
    {
        {Type.Sonar, 80f},
        {Type.Wood, 25f},
        {Type.Ospirit, 60f},
    };
}
