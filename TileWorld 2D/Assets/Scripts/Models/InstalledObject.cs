using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Installed objects are things like walls, doors, furniture etc */

public class InstalledObject
{

    Tile tile;  // This represents the base tile of the object -- but in practice large objects may
                // occupy multiple tiles

    // this object type will be queried by the visual system to know what sprite to render for this object
    string objectType;

    // This is a multiplier. So a value of "2" here, means you move twice as slowly ( i.e. at half speed)
    // Tile types and other environmental effetces may be combined.
    // For example a "rough" tile ( cost of 2) with table (cost of 3) that is on fire (cost of 3)
    // would have a movement cost of (2+3+3 = 8), so you move through this tile at 1/8th normal speed.
    // SPECIAL: If movement cost is = 0, this object is inpasable (e.g. a wall)
    float movementCost;

    // For example a sofa may be 3X2 (actual graphics only appear to cover the 1X3 area, but te extra row is for leg room)
    int width;
    int height;

    // Todo: implement larger objects
    // Todo: implement object rotation

    // Protected constructor to prevent direct creation of installed objects
    protected InstalledObject() { }

    static public InstalledObject CreatePrototype(string objectType, float movementCost = 1f, int width = 1, int height = 1)
    {
        var obj = new InstalledObject()
        {
            objectType = objectType,
            movementCost = movementCost,
            width = width,
            height = height
        };
        return obj;
    }

    static public InstalledObject PlaceInstance(InstalledObject proto, Tile tile)
    {
        var obj = new InstalledObject()
        {
            objectType = proto.objectType,
            movementCost = proto.movementCost,
            width = proto.width,
            height = proto.height,

            tile = tile
        };

        // FixMe: this assumes 1x1
        if (tile.PlaceObject(obj) == false)
        {
            // Could not place object in tile, prob occupied already

            // Do not return our newly instantiated object, it will be garbage collected
            return null;
        }

        return obj;
    }
}
