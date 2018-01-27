using UnityEngine;
using UnityEngine.Assertions;

public class PopUpManager : MonoBehaviour {

    [Tooltip("Intervalle before next popup spam spawn in seconds")]
    public Vector2 intervalle;
    [Tooltip("Canvas")]
    public Canvas canvas;
    
    private float timer;
    private float refTimer;

    private GameObject samplePopUp;

    private void ResetTimer()
    {
        timer = 0.0f;
        refTimer = Random.Range(intervalle.x, intervalle.y);
    }

    private void Start()
    {
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y, "Intervalle lower bound must be lower than highter bound.");
        samplePopUp = Resources.Load("Popup/EmptyPopUp") as GameObject;
        ResetTimer();
    }

    private void Update ()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
        {
            AddAnnoyingPopup();
            refTimer = Random.Range(intervalle.x, intervalle.y);
        }
	}

    private void AddAnnoyingPopup()
    {
        AddPopup(samplePopUp, "Annoying Popup");
    }

    private void AddPopup(GameObject go, string popupName)
    {
        GameObject pu = Instantiate(go, Vector3.zero, Quaternion.identity);
        pu.name = popupName;
        pu.transform.SetParent(canvas.transform, false);
        pu.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        RectTransform canvasPos = canvas.transform as RectTransform;
        ResetTimer();
    }
}
