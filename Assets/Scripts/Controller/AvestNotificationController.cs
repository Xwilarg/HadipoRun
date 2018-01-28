using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvestNotificationController : MonoBehaviour {

	public float speed;
	public float yMin, yMax;
	private bool isOpen;
	private RectTransform panelRectTransform;
	// Use this for initialization
	void Start () {
		isOpen = false;
		panelRectTransform = transform as RectTransform;
		panelRectTransform.localPosition = new Vector2 (panelRectTransform.localPosition.x, yMin);
	}

	// Update is called once per frame
	void Update () {
		if (isOpen) {
			if (panelRectTransform.localPosition.y < yMax) {
				panelRectTransform.localPosition = new Vector2(panelRectTransform.localPosition.x, panelRectTransform.localPosition.y + Time.deltaTime * speed);
			}
		} else {
			if (panelRectTransform.localPosition.y > yMin) {
				panelRectTransform.localPosition = new Vector2(panelRectTransform.localPosition.x, panelRectTransform.localPosition.y - Time.deltaTime * speed);
			}
		}
	}

	void Open() {
		isOpen = true;
	}

	void Close() {
		isOpen = false;
	}
}
