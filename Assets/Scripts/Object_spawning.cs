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
	[Range(0, 100)]
    public float SpawnDelay;
    float currTime;
    public bool left;
    public bool center;
    public bool right;

	private Collider2D col2D;
    // Use this for initialization 
    void Start()
    {
        currTime = SpawnDelay;
		col2D = GetComponent<Collider2D> ();
    }

    // Update is called once per frame 
    void Update()
    {
		int maxX = Random.Range((int)(col2D.transform.localScale.x * -1), (int)col2D.transform.localScale.x);
        int obj = Random.Range(0, Spawnable.Count);
        //int yAxis = rand.Next(0, (int)zone);
		Vector2 spawnPlace = new Vector2(col2D.transform.position.x + maxX, col2D.transform.position.y);

        Vector2 spawnPlace = new Vector2(maxX, yAxis);

        if (currTime <= 0)
        {
            Instantiate(Spawnable[obj], spawnPlace, Quaternion.identity);
            currTime = SpawnDelay;
        }
        else
            currTime -= Time.deltaTime;
    }

    private float max_range()
    {
        float tier = col2D.transform.localScale.x / 3;
        float total = col2D.transform.localScale.x;

        if (left && center && right)
            return Random.Range(col2D.transform.localScale.x * -1, col2D.transform.localScale.x);
        else if (left && center && !right)
            return Random.Range(col2D.transform.localScale.x * -1, total - (tier * 2));
        else if (!left && center && right)
            return Random.Range(total - (tier * 2), col2D.transform.localScale.x);
        else if (left && !center && right)
            return (Random.Range(Random.Range(total * -1, total - tier), Random.Range(total - tier, total)));
        else if (left && !center && !right)
            return Random.Range(col2D.transform.localScale.x * -1, total - (tier * 2));
        else if (!left && center && !right)
            return (Random.Range(total - (tier * 2), total - tier));
        else
            return (Random.Range(total - tier, total));
    }
}