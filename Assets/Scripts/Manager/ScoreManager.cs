using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    private float score;
    private string nickname;

	private void Start ()
    {
        score = 0.0f;
	}
	
	public void improveScore(float addScore)
    {
        score += addScore;
    }

    public void gameOverBSOD()
    {
        if (!File.Exists("bestScores.dat"))
        {
            for (int i = 0; i < 10; i++)
            {
                File.AppendAllText("bestScores.dat", "None" + System.Environment.NewLine);
                File.AppendAllText("bestScores.dat", "0" + ((i == 9) ? ("") : (System.Environment.NewLine)));
            }
        }
        string[] allLines = File.ReadAllLines("bestScores.dat");
        for (int i = 0; i < 20; i+=2)
        {
            if (score > System.Convert.ToSingle(allLines[i + 1]))
            {
                int y = 16;
                for (; y >= i; y-=2)
                {
                    allLines[y + 2] = allLines[y];
                    allLines[y + 3] = allLines[y + 1];
                }
                y += 2;
                allLines[y] = System.Environment.MachineName;
                allLines[y + 1] = allLines[y + 1] = score.ToString();
            }
        }
        File.WriteAllLines("bestScores.dat", allLines);
        Screen.fullScreen = true;
        SceneManager.LoadSceneAsync("GameOver");
    }
}
