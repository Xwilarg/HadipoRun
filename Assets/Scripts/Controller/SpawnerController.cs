using UnityEngine;
using UnityEngine.Analytics;

public class SpawnerController : MonoBehaviour {

	// Use this for initialization
	public GameObject[] ItemList;

	private float count;
	private float lenght;


	void Start () {
	//	ItemList = gameObject.GetComponents<ItemSpawn> ();
		lenght = transform.localScale.x * 2;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject item in ItemList) {
			ItemSpawn spawn = item.GetComponent<ItemSpawn> ();

			if (spawn.canBeInstantiate (Time.deltaTime) == true) {
				
				if (spawn.spawnPatern == ItemSpawn.SpawnPatern.SINGLE) {
					SpawnSingle (item);
				} else if (spawn.spawnPatern == ItemSpawn.SpawnPatern.WALL) {
					SpawnWall (item);
				}
			}
		}
	}

	void  SpawnSingle(GameObject item){
		float maxX = Random.Range (-transform.localScale.x, transform.localScale.x);
		Vector2 spawnPlace = new Vector2 (transform.position.x + maxX, transform.position.y);
		Instantiate (item, spawnPlace, Quaternion.identity);
	}

	void SpawnWall(GameObject item) {
		float wallLenght = lenght / 3;
		float itemLenght = (item.GetComponent<BoxCollider2D> ().transform.localScale.x);
		float pos = Random.Range(transform.position.x - lenght / 2, transform.position.x + (lenght / 2) - wallLenght);
		Vector2 spawnPlace;

		for (float i = 0; i < wallLenght; i += itemLenght / 4) {
			spawnPlace.x = pos + i;
			spawnPlace.y = transform.position.y;
			Instantiate (item, spawnPlace, Quaternion.identity);
		}
	}
}

	