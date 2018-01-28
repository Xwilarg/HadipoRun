using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMouvement : MonoBehaviour {

    float refTime;
    float currTime;
    LineRenderer lr;
	void Start () {
        refTime = 3.0f;
        currTime = 0.0f;
        lr = new LineRenderer();
        Vector3[] pos = { transform.position, transform.position + Vector3.down * 10 };
        //lr.SetPositions(pos);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
