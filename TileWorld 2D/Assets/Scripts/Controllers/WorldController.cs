using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : Singleton<WorldController> {

    public int Width = 20;
    public int Height = 20;

    private World world;

    // Use this for initialization
    void Start () {
        world = new World(Width, Height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
