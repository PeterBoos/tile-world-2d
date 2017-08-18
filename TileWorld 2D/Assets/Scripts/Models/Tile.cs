using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public enum TileType { Grass, Dirt, Stone }
    public TileType Type { get; set; }

    public Vector3 Position { get; private set; }

    InstalledObject installedObject;

    public int X { get; private set; }
    public int Y { get; private set; }

    public InstalledObject InstalledObject
    {
        get
        {
            return installedObject;
        }

        set
        {
            installedObject = value;
        }
    }

    public Tile(Vector3 position, TileType type)
    {
        Position = position;
        Type = type;
        X = (int)position.x;
        Y = (int)position.y;
    }

    public bool PlaceObject(InstalledObject objInstance)
    {
        if (objInstance == null)
        {
            installedObject = null;
            return true;
        }

        if (installedObject != null)
        {
            Debug.LogError("Trying to assign a installed object to a tile that already has one!");
            return false;
        }

        installedObject = objInstance;
        return true;
    }
}
