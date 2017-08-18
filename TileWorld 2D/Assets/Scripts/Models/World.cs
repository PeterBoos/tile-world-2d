using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	public int Width { get; private set; }
    public int Height { get; private set; }

    Tile[,] tiles;

    Dictionary<string, InstalledObject> installedObjectPrototypes;

    public World(int width, int height)
    {
        Width = width;
        Height = height;
        tiles = new Tile[Width, Height];

        SpawnTiles();

        Debug.Log("World was created with " + tiles.Length + " (" + Width + "*" + Height + ") tiles");        
    }

    void CreateInstalledObjectPrototypes()
    {
        installedObjectPrototypes = new Dictionary<string, InstalledObject>();

        InstalledObject wallPrototype = InstalledObject.CreatePrototype(
            "Wall",
            0,
            1,
            1);

        installedObjectPrototypes.Add("Wall", 
            InstalledObject.CreatePrototype(
                                    "Wall",
                                    0,
                                    1,
                                    1)
            );
    }

    void SpawnTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var position = new Vector3(x, y, 0);
                Tile.TileType defaultTileType = Tile.TileType.Grass;
                tiles[x, y] = new Tile(position, defaultTileType);
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x < 0 ||x > Width)
        {
            // Out of bounds
            Debug.LogError("Tile position out of bounds");
            return null;
        }
        if (y < 0 || y > Height)
        {
            // Out of bounds
            Debug.LogError("Tile position out of bounds");
            return null;
        }

        return tiles[x, y];
    }
}
