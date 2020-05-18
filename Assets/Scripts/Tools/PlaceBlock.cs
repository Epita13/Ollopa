using Godot;
using System;

public static class PlaceBlock
{


    public static bool CanPlace(int x, int y, out bool res)
    {
        Block b = World.GetBlock(x,y);
        res = b.GetType==Block.Type.Air;
        res = res && (b.isAutoGenerated ||  HasNeighbors(b));
        res = res && !HasBuilding(x, y);
        return res;
    }

    private static bool HasNeighbors(Block b)
    {
        bool res = false;
        if (World.GetBlock(b.x-1, b.y).GetType!=Block.Type.Air)
            res = true;
        if (World.GetBlock(b.x+1, b.y).GetType!=Block.Type.Air)
            res = true;
        if (b.y-1>=0 && World.GetBlock(b.x, b.y-1).GetType!=Block.Type.Air)
            res = true;
        if (b.y+1<Chunk.chunkMax && World.GetBlock(b.x, b.y+1).GetType!=Block.Type.Air)
            res = true;
        return res;
    }

    public static bool Place(int x, int y, Block.Type type)
    {
        int displayX = x;
        int displayY = y;
        if (x < 0)
            x = World.size * Chunk.size + x;
        else if (x >= World.size * Chunk.size)
            x = x - World.size * Chunk.size;
        bool res;
        if (CanPlace(x, y, out res))
        {
            World.GetChunk(x).AddBlock(Chunk.GetLocaleX(x), y, displayX, displayY, type);
            Save._Save(World.saveName);
            Delay.StartDelay(World.BlockTilemap, 0.3f,
                () =>
                {
                    World.UI2BlockTilemap.SetCell(x, -y+Chunk.height, -1);
                },
                (delta, time) =>
                {
                    int tile = Mathf.FloorToInt(delta * 16 / time);
                    World.UI2BlockTilemap.SetCell(x, -y+Chunk.height, tile);
                }
            );
            GD.Print(World.UI2BlockTilemap == null);
        }
        return res;
    }

    private static bool HasBuilding(int x, int y)
    {
        Vector2 vec = Convertion.World2WorldBorn(new Vector2(x, y));
        x = (int)vec.x;
        y = (int)vec.y;
        int i = 0;
        bool res = false;
        while (i < World.placedBuildings.Count)
        {
            Building b = World.placedBuildings[i];
            if (b.IsInBuilding(x, y))
                res = true;
            i++;
        }

        return res;
    }

}
