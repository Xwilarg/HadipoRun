using UnityEngine;

public class ItemSpawn : MonoBehaviour{

	[Tooltip("Time min beetween spawn")]
	public float spawnDelayMin;
	[Tooltip("Time max beetween spawn")]
	public float spawnDelayMax;

	public enum SpawnPatern {
		SINGLE,
		WALL,
	}

	public SpawnPatern spawnPatern;
	private float count;

	void Start() 
	{
		count = Random.Range(spawnDelayMin, spawnDelayMax);
	}

	public bool canBeInstantiate (float delay)
	{
		bool action = false;

		if (count <= 0) {
			action = true;
			count = Random.Range(spawnDelayMin, spawnDelayMax);;
		} else
			count -= delay;
		return action;
	}
}
