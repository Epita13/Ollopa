using Godot;
using System;
using System.Collections.Generic;


public class Chunk
{
    /* Constante */
    public static readonly int size = 16;
    public int id;

    private const int chunkMax = 0;
    private const int chunkMin = -100;
    private const int seaLevel = -80;
    private const int minYGeneration = -85;
    private const int maxYGeneration = -60;


    private List<List<Block>> blocks;

    public Chunk(int id)
    {
        this.id = id;
        Generate();
    }

    public void Generate()
    {

        OpenSimplexNoise noise = World.noise;
        blocks = new List<List<Block>>();
        
        for (int x = 0; x < size; x++)
        {
            int maximumY = GetMaximumY(x, noise);
            blocks.Add(new List<Block>());
            for (int y = chunkMin; y <= chunkMax; y++)
            {
                if (y<=maximumY-6)
                {
                    blocks[x].Add(new Block(0, x+(id*size), y));
                }else if (y<=maximumY-1)
                {
                    blocks[x].Add(new Block(2, x+(id*size), y));
                }else if (y==maximumY)
                {
                    blocks[x].Add(new Block(1, x+(id*size), y));
                }
            }
        }
        
    }
    public void Draw()
    {
        TileMap Ground = World.tilemp_blocks;
        foreach (var colon in blocks)
        {
            foreach (var block in colon)
            {
                Ground.SetCell(block.x, -block.y, block.tileId);
            }
        }
    }


    private int GetMaximumY(int x, OpenSimplexNoise noise)
    {
        float rayon = (World.size*size) / (2*Mathf.Pi);
        float step = (2*Mathf.Pi)/ (World.size*size);
        float angle = (x+(id*size))*step;
        Vector2 point = new Vector2(Mathf.Cos(angle)*rayon, Mathf.Sin(angle)*rayon);
        float grayLevel = noise.GetNoise2dv(point) + 1.0f; // [0,2.0]
        float a = (maxYGeneration-minYGeneration)/2; // coeficient directeur
        float b = minYGeneration; // image a l'origine
        return (int)((a*grayLevel)+b);

    }
}