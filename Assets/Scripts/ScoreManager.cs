using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private int score;

	private void Start ()
    {
        score = 0;
	}
	
	public void improveScore(int addScore)
    {
        score += addScore;
    }
}
