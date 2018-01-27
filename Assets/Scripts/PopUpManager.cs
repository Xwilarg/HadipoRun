using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour {

    [Tooltip("Intervalle before next popup spam spawn in seconds")]
    public Vector2 intervalle;
    [Tooltip("Canvas")]
    public Canvas canvas;

    private List<PopUp> popUps;
    private float timer;
    private float refTimer;

    private void ResetTimer()
    {
        timer = 0.0f;
        refTimer = Random.Range(intervalle.x, intervalle.y);
    }

    private void Start()
    {
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y, "Intervalle lower bound must be lower than highter bound.");
        popUps = new List<PopUp>();
        ResetTimer();
    }

    private void Update ()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
        {
            AddPopup();
            refTimer = Random.Range(intervalle.x, intervalle.y);
        }
	    foreach (PopUp pu in popUps)
        {
            pu.Update();
        }
	}

    private void AddPopup()
    {
        GameObject pu = new GameObject("Annoying PopUp", typeof(Image));
        pu.transform.parent = canvas.transform;
        Image img = pu.GetComponent<Image>();
        RectTransform rt = pu.GetComponent<RectTransform>();
        img.sprite = Resources.Load<Sprite>("Popup/ErrorPopupSimple");
        img.SetNativeSize();
        rt.localPosition = Vector3.zero;
        rt.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        ResetTimer();
    }
}
