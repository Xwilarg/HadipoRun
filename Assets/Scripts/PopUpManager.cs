using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {

    List<PopUp> popUps;

    private void Start()
    {
        popUps = new List<PopUp>();
    }

    private void Update ()
    {
	    foreach (PopUp pu in popUps)
        {
            pu.Update();
        }
	}
}
