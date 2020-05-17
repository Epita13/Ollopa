using Godot;
using System;
using System.Collections.Generic;

public static class Sounds
{
    /*Player sounds*/
    public enum Type
    {
        PlayerDeath,
        PlayerGetloot,
        PlayerLaser,
        BlockBreak,
    }
    public static Dictionary<Type, AudioStream> sounds = new Dictionary<Type, AudioStream>
    {
        {Type.PlayerDeath, GD.Load<AudioStream>("res://Assets/Ressources/Sounds/Player/player_death.wav")},
        {Type.PlayerGetloot, GD.Load<AudioStream>("res://Assets/Ressources/Sounds/Player/player_getloot.wav")},
        {Type.PlayerLaser, GD.Load<AudioStream>("res://Assets/Ressources/Sounds/Player/player_laser.wav")},
        {Type.BlockBreak, GD.Load<AudioStream>("res://Assets/Ressources/Sounds/Blocks/BlockBreak.wav")},
    };
    
    public static Dictionary<Type, float> soundAjust = new Dictionary<Type, float>
    {
        {Type.PlayerDeath, 0},
        {Type.PlayerGetloot, -30},
        {Type.PlayerLaser, 0},
        {Type.BlockBreak, -20},
    };
}
