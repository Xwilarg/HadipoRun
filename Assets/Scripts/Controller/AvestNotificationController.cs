using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvestNotificationController : MonoBehaviour {

	public float delay = 3.0f;
	public float speed;
	public float yMin, yMax;
	private bool isOpen;
	private bool isShowing;
	private RectTransform panelRectTransform;
	private float currentDelay = 0.0f;
	// Use this for initialization
	void Start () {
		isOpen = false;
		isShowing = false;
		panelRectTransform = transform as RectTransform;
		panelRectTransform.localPosition = new Vector2 (panelRectTransform.localPosition.x, yMin);

	}

	void Show() {
		if (isShowing) {
			return;
		} else {
			currentDelay = 0.0f;
			isShowing = true;
			Open ();
		}	
	}

	// Update is called once per frame
	void Update () {
		if (isOpen) {
			if (panelRectTransform.localPosition.y < yMax) {
				panelRectTransform.localPosition = new Vector2 (panelRectTransform.localPosition.x, panelRectTransform.localPosition.y + Time.deltaTime * speed);
			} if (isShowing) {
				currentDelay += Time.deltaTime;
				if (currentDelay > delay) {
					isShowing = false;
					Close ();
				}
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
