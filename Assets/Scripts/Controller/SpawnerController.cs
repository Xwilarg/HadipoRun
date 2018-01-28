using UnityEngine;
using UnityEngine.Analytics;

public class SpawnerController : MonoBehaviour {

	// Use this for initialization
	public GameObject[] ItemList;	
	private float count;

	void Start () {
	//	ItemList = gameObject.GetComponents<ItemSpawn> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject item in ItemList) {
			ItemSpawn spawn = item.GetComponent<ItemSpawn> ();
			if (spawn.canBeInstantiate (Time.deltaTime) == true) {
				float maxX = Random.Range(-transform.localScale.x, transform.localScale.x);
				Vector2 spawnPlace = new Vector2(transform.position.x + maxX, transform.position.y);

				Instantiate(item, spawnPlace, Quaternion.identity);
			}
		}
	}
}
