using Godot;
using System;
using System.Collections.Generic;

public static class World
{
    public static TileMap tilemp_blocks;
    public static Random random;

    // SimplexNoise
    public static OpenSimplexNoise noise = new OpenSimplexNoise();
    public static int seed;
    public const int octave = 3;
    public const float periode = 20.0f;
    public const float persistence = 0.25f;
    public const float lacunarity = 3.5f;
    
    // size en nombre de chunks
    public static int size;
    public static List<Chunk> chunks;

    /// Initialise le monde et le calcule.
    public static void Init(int size, TileMap tilemp_blocks, int seed = -1)
    {
        // Random et seed
        if (seed==-1){
            World.random = new Random();
            World.seed = random.Next();
        }else{
            World.random = new Random(seed);
            World.seed = seed;
        }

        World.size = size;
        World.tilemp_blocks = tilemp_blocks;
        World.chunks = new List<Chunk>();

        // Initialisation du SimplexNoise
        noise.SetSeed(World.seed);
        noise.SetOctaves(octave);
        noise.SetPeriod(periode);
        noise.SetPersistence(persistence);
        noise.SetLacunarity(lacunarity);

        World.Generate();
    }
    
    /// Calcule le monde en fonction des parametres.
    private static void Generate()
    {
        for (int x = 0; x < size; x++)
        {
            Chunk instance_chunk = new Chunk(x);
            chunks.Add(instance_chunk);
        }
    }

    /// Dessine le monde sur la scene.
    public static void Draw()
    {
        foreach (Chunk chunk in chunks)
            chunk.Draw();
    }
}
