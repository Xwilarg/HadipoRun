using UnityEngine;
using UnityEngine.UI;

public class hadistrike : MonoBehaviour {
    public Text Count_strike;
    public int strikes;
	// Use this for initialization
	void Start () {
        strikes = 0;
        Count_strike.text = strikes.ToString();
	}

    public void UpdateStrike()
    {
        GetComponent<PopUpManager>().AddAlert("Illegal downloading detected", "Warning: illegal operations has been detected from your internet connexion.");
        strikes++;
        Count_strike.text = strikes.ToString();
    }
}
