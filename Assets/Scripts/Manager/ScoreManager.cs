using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private float score;

	private void Start ()
    {
        score = 0.0f;
	}
	
	public void improveScore(float addScore)
    {
        score += addScore;
    }

	public void gameOverBSOD() {
	}
}
