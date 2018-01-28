using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableController : MonoBehaviour {
	public void Close() {
		Destroy (gameObject);
	}
}
