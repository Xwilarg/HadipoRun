using System.Collections.Generic;
using UnityEngine;

public class Object_spawning : MonoBehaviour
{
    [Tooltip("Objets to spawn")]
    public List<GameObject> Spawnable;

    [Tooltip("Range of spawn")]
    [Range(0, 100)]
    public float zone;
    UnityEngine.Random rand;
    [Tooltip("Time beetween spawn")]
	[Range(0, 10)]
    public float SpawnDelay;
    float currTime;
    public bool left;
    public bool center;
    public bool right;

    void Start()
    {
        currTime = SpawnDelay;
    }

    // Update is called once per frame 
    void Update()
	{
		if (currTime <= 0)
		{
			float maxX = Random.Range(-transform.localScale.x, transform.localScale.x);
	        int obj = Random.Range(0, Spawnable.Count);
	        //int yAxis = rand.Next(0, (int)zone);
			Vector2 spawnPlace = new Vector2(transform.position.x + maxX, transform.position.y);

            Instantiate(Spawnable[obj], spawnPlace, Quaternion.identity);
            currTime = SpawnDelay;
        }
        else
            currTime -= Time.deltaTime;
    }
}