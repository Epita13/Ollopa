using Godot;
using System;
using System.Collections.Generic;

public class Paralax : Sprite
{
    
    [Export] public float speed = 0;
    
    public override void _Ready()
    {
        if (!PlayerMouvements.HasPlayer)
            throw new UninitializedException("Paralax", "PlayerMouvement");
        Position = new Vector2(PlayerMouvements.instance.Position.x, Position.y);
    }

    public override void _Process(float delta)
    {
         
    }
}
