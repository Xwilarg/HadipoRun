using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocusableController : MonoBehaviour, IPointerDownHandler {

	private RectTransform panel;

	void Awake () {
		panel = GetComponent <RectTransform> ();  
	}

	public void OnPointerDown (PointerEventData data) {
		panel.SetAsLastSibling ();
	}

}