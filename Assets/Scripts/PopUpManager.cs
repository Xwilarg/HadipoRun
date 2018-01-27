using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {

    [Tooltip("Intervalle before next popup spam spawn in seconds")]
    public Vector2 intervalle;

    private List<PopUp> popUps;
    private float timer;
    private float refTimer;

    private void Start()
    {
        popUps = new List<PopUp>();
        timer = 0.0f;
        refTimer = Random.Range(intervalle.x, intervalle.y);
    }

    private void Update ()
    {
        timer += refTimer
	    foreach (PopUp pu in popUps)
        {
            pu.Update();
        }
	}
}
