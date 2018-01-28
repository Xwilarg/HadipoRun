using UnityEngine;

public class ItemSpawn : MonoBehaviour{

	[Tooltip("Time min beetween spawn")]
	public float spawnDelayMin;
	[Tooltip("Time max beetween spawn")]
	public float spawnDelayMax;

    [Tooltip("Step before item is allowed")]
    public float scoring;
    private ScoreManager sm = null;

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
        if (sm == null)
            sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        if (sm.getScore() < scoring)
            return (false);
		bool action = false;

		if (count <= 0) {
			action = true;
			count = Random.Range(spawnDelayMin, spawnDelayMax);;
		} else
			count -= delay;
		return action;
	}
}
