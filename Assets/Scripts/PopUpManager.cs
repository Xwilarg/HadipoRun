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
        samplePopUp = Resources.Load("Popup/Popup") as GameObject;
        ResetTimer();
    }

    private void Update ()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)        {
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
        pu.GetComponent<PopupScript>().setDownloadVars(Random.Range(2, 10), Random.Range(10, 1000));
        pu.name = popupName;
        pu.transform.SetParent(canvas.transform, false);
        RectTransform canvasTranform = canvas.transform as RectTransform;
        RectTransform puTranform = pu.transform as RectTransform;
        Vector2 minRatio = new Vector2((puTranform.rect.width / 2) / canvasTranform.rect.width, (puTranform.rect.height / 2) / canvasTranform.rect.height);
        Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
        puTranform.anchorMin = spawnPoint;
        puTranform.anchorMax = spawnPoint;
        ResetTimer();
    }
}