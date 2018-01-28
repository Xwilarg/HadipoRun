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
    private PopUpManager pum;

	void Start () {
        pum = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpManager>();
		isOpen = false;
		isShowing = false;
		panelRectTransform = transform as RectTransform;
		panelRectTransform.anchoredPosition = new Vector2 (panelRectTransform.anchoredPosition.x, yMin);

	}

	public void Show() {
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
			if (panelRectTransform.anchoredPosition.y < yMax) {
				panelRectTransform.anchoredPosition = new Vector2 (panelRectTransform.anchoredPosition.x, panelRectTransform.anchoredPosition.y + Time.deltaTime * speed);
			} else if (isShowing) {
				currentDelay += Time.deltaTime;
				if (currentDelay > delay) {
					isShowing = false;
					Close ();
				}
			}
		} else {
			if (panelRectTransform.anchoredPosition.y > yMin) {
				panelRectTransform.anchoredPosition = new Vector2(panelRectTransform.anchoredPosition.x, panelRectTransform.anchoredPosition.y - Time.deltaTime * speed);
			}
		}
	}

	void Open() {
		isOpen = true;
	}

	void Close() {
		isOpen = false;
	}

    public void AvastPopup()
    {
		pum.AddInfo("Avest! information", "A new version of virus database has been installed.");
    }
}
