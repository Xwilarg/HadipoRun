using UnityEngine;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	private ItemSpawn[] ItemList;	
	private float count;

	void Start () {
		ItemList = gameObject.GetComponents<ItemSpawn> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (ItemSpawn item in ItemList) {
			if (item.canBeInstantiate (Time.deltaTime) == true) {
				float maxX = Random.Range(-transform.localScale.x, transform.localScale.x);
				Vector2 spawnPlace = new Vector2(transform.position.x + maxX, transform.position.y);

				Instantiate(item.Item, spawnPlace, Quaternion.identity);
			}
		}
	}
}
