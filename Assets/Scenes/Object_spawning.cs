using System;
using System.Collections.Generic;
using UnityEngine;

public class Object_spawning : MonoBehaviour {
    [Tooltip("Objets to spawn")]
    public List<GameObject> Spawnable;

    [Tooltip("Range of spawn")] [Range(0, 100)]
    public float zone;
    System.Random rand;
    [Tooltip("Time beetween spawn")]
    public float SpawnDelay;
    float currTime;

	// Use this for initialization
	void Start () {
        rand = new System.Random();
        currTime = SpawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
        int obj = rand.Next(Spawnable.Count);
        int xAxis = rand.Next(0, (int)zone);
        Vector2 spawnPlace = new Vector2(xAxis, 0);

        if (currTime <= 0)
        {
            Instantiate(Spawnable[obj], spawnPlace, Quaternion.identity);
            currTime = SpawnDelay;
        }
        else
            currTime -= Time.deltaTime;
	}
}
