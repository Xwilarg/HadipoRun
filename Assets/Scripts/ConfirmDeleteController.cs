using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDeleteController : MonoBehaviour {

	void onClickNo() {
		Destroy (gameObject);
	}

	void onClickYes() {
		ScoreManager sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();

	}
}
