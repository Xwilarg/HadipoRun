using UnityEngine;

public class ItemSpawn : MonoBehaviour{

	[Tooltip("Time beetween spawn")]
	public float spawnDelay;
	[Tooltip("Item to create spawn")]
	public GameObject Item;

	private float count;

	public ItemSpawn () 
	{
		count = spawnDelay;
	}

	public bool canBeInstantiate (float delay)
	{
		bool action = false;

		if (count <= 0) {
			action = true;
			count = spawnDelay;
		} else
			count -= delay;
		return action;
	}
}
