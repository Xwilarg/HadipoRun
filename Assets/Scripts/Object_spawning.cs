using System;
using System.Collections.Generic;
using UnityEngine;

public class Object_spawning : MonoBehaviour
{
    [Tooltip("Objets to spawn")]
    public List<GameObject> Spawnable;

    [Tooltip("Range of spawn")]
    [Range(0, 100)]
    public float zone;
    System.Random rand;
    [Tooltip("Time beetween spawn")]
	[Range(0, 100)]
    public float SpawnDelay;
    float currTime;

	private Collider2D col2D;
    // Use this for initialization 
    void Start()
    {
        rand = new System.Random();
        currTime = SpawnDelay;
		col2D = GetComponent<Collider2D> ();
    }

    // Update is called once per frame 
    void Update()
    {
		int maxX = rand.Next((int)(col2D.transform.localScale.x * -1), (int)col2D.transform.localScale.x);
        int obj = rand.Next(Spawnable.Count);
        //int yAxis = rand.Next(0, (int)zone);
		Vector2 spawnPlace = new Vector2(maxX, col2D.transform.position.y);

        if (currTime <= 0)
        {
            Instantiate(Spawnable[obj], spawnPlace, Quaternion.identity);
            currTime = SpawnDelay;
        }
        else
            currTime -= Time.deltaTime;
    }
}