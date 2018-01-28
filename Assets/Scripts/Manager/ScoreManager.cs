using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    private float score;
    private string nickname;

    private float hadipoScore;
    public Slider hadipoSlider;
    public float maxHadipoScore;

    public void increaseHadipoScore(float increase)
    {
        hadipoScore += increase;
        hadipoSlider.value = hadipoScore * 100 / maxHadipoScore;
    }

	//Hadipo tracking
	private float conspicuousity;
	private uint seededCount;
	[Tooltip("Conspicuousity grows by x * n, n being the number of seeded downloads.")]
	public float conspicuousityFactor;
	[Tooltip("Conspicuousity falls by x per seconds if no downloads are seeded.")]
	public float stealthFactor;

	private Text conspicuousityText;
	private uint strikes;
	private float strikeImmunity;

	public void sprout()
	{
		++seededCount;
	}

	public void wither()
	{
		--seededCount;
	}

	private void getTracked(float deltaTime)
	{
		if (strikeImmunity > 0.0f)
			strikeImmunity -= deltaTime;
		else if (seededCount == 0)
			conspicuousity -= stealthFactor * Time.deltaTime;
		else
			conspicuousity += conspicuousityFactor * seededCount * Time.deltaTime;
		if (conspicuousity > 1000)
		{
			strikes++;
			conspicuousity = 0.0f;
		}
	}

	private void Update()
	{
		//conspicuousityText.text = System.String.Concat("Seeding: ", conspicuousity);
	}

	//End Hadipo tracking

	private void Start ()
    {
        score = 0.0f;
        hadipoScore = 0.0f;
		//Hadipo tracking
		conspicuousityText = GameObject.Find("LeftCanvas").GetComponentInChildren<Text>();
		conspicuousity = 0.0f;
		seededCount = 0;
		strikes = 0;
		strikeImmunity = 0.0f;
	}
	
	public void improveScore(float addScore)
    {
        score += addScore;
    }

	public void gameOverHadipo()
	{
		gameOverBSOD (); //TODO Differentiate endings
	}

	public void gameOverBSOD()
	{
		gameOver ();
		Screen.fullScreen = true;
		SceneManager.LoadSceneAsync("GameOver");
	}
    
	public void gameOver()
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
    }
}
