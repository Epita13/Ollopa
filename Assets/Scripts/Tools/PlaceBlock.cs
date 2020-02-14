using Godot;
using System;

public static class PlaceBlock
{


    public static bool CanPlace(int x, int y, out bool res)
    {
        Block b = World.GetBlock(x,y);
        res = b.type==Block.Type.Air;
        return res;
    }

    public static bool Place(int x, int y, Block.Type type)
    {
        bool res;
        if (CanPlace(x, y, out res))
        {
            World.GetChunk(x).AddBlock(Chunk.GetLocaleX(x), y, type);
        }
        return res;
    }

}
