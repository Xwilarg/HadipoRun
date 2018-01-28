using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sys32Controller : MonoBehaviour {

	public void onClickNo() {
		Destroy (gameObject);
	}

	public void onClickYes() {
		ScoreManager sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
		sm.gameOverBSOD ();
	}
}
