using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {
	public Text windowTitle;
	public Text windowDescription;

	public PopupController setTitle (string title)
	{
		this.windowTitle.text = title;
		return this;
	}

	public PopupController setDescription (string description)
	{
		this.windowDescription.text = description;
		return this;
	}
}
