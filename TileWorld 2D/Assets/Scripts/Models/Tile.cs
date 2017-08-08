using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public enum TileType { Grass, Dirt, Stone }
    public TileType Type { get; set; }

    public Vector3 Position { get; private set; }

    public Tile(Vector3 position, TileType type)
    {
        Position = position;
        Type = type;
    }
}
