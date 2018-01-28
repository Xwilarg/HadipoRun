using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigErrorController : MonoBehaviour {
	public Text errorText;

	public BigErrorController setErrors (string errors)
	{
		this.errorText.text = errors;
		return this;
	}
}
