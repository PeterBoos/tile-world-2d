using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : Singleton<WorldController> {

    public int Width = 20;
    public int Height = 20;

    /// <summary>
    /// List with available tile sprites
    /// 0 - grass01
    /// 1 - grass02
    /// 2 - dirt01
    /// </summary>
    public List<Sprite> TileSprites;

    public List<Sprite> TreeSprites;

    public List<Sprite> RockSprites;

    private World world;

    // Use this for initialization
    void Start () {
        world = new World(Width, Height);
        SpawnSprites();
        SpawnTrees();
        SpawnRocks();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnSprites()
    {
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                var tile = world.GetTileAt(x, y);
                var tile_go = new GameObject("Tile_" + x + "_" + y);
                tile_go.transform.position = tile.Position;
                tile_go.transform.SetParent(this.transform);
                var sr = tile_go.AddComponent<SpriteRenderer>();
                sr.sortingLayerName = "Tiles";
                var tileSpriteIndex = (int)Random.Range(0, 3);
                if (tileSpriteIndex == 0)
                {
                    sr.sprite = TileSprites[0];
                }
                else if (tileSpriteIndex == 1)
                {
                    sr.sprite = TileSprites[1];
                }
                else if(tileSpriteIndex == 2)
                {
                    sr.sprite = TileSprites[2];
                }
            }
        }
    }

    void SpawnTrees()
    {
        var position = new Vector3(10f, 10f, 0f);
        var tree_go = new GameObject("Tree");
        tree_go.transform.position = position;
        var sr = tree_go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Trees";
        sr.sprite = TreeSprites[0];
    }

    void SpawnRocks()
    {
        var position = new Vector3(12f, 13f, 0f);
        var rock_go = new GameObject("Rock");
        rock_go.transform.position = position;
        var sr = rock_go.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = "Trees";
        sr.sprite = RockSprites[0];
    }
}
