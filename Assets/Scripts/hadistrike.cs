using System.Collections;
using System.Collections.Generic;
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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStrike()
    {
        strikes++;
        Count_strike.text = strikes.ToString();
    }
}
