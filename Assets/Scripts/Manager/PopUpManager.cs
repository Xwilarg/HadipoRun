using UnityEngine;
using UnityEngine.Assertions;

public class PopUpManager : MonoBehaviour {

    [Tooltip("Intervalle before next popup spam spawn in seconds")]
    public Vector2 intervalle;
    [Tooltip("Intervalle before next avest popup spawn in seconds")]
    public Vector2 inAvest;
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
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0 && inAvest.x > 0 && inAvest.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y && inAvest.x < inAvest.y, "Intervalle lower bound must be lower than highter bound.");
        samplePopUp = Resources.Load("Popup/DownloadPopup") as GameObject;
        ResetTimer();
    }

    private void Update ()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
        {
			AddAnnoyingPopup();
            ResetTimer();
        }
	}

    private void AddAnnoyingPopup()
    {
        //AddPopup(samplePopUp, "Annoying Popup");
    }

    public void AddDownloadingPopup(string title, float size)
    {
        AddPopup(samplePopUp, "Downloading Popup", title, size);
    }

    private void AddPopup(GameObject go, string popupName, string windowName, float fileSize = 0)
    {
        GameObject pu = Instantiate(go, Vector3.zero, Quaternion.identity);
        pu.GetComponent<PopupScript>().setDownloadVars(fileSize, windowName);
        pu.name = popupName;
        pu.transform.SetParent(canvas.transform, false);
        RectTransform canvasTranform = canvas.transform as RectTransform;
        RectTransform puTranform = pu.transform as RectTransform;
        Vector2 minRatio = new Vector2((puTranform.rect.width / 2) / canvasTranform.rect.width, (puTranform.rect.height / 2) / canvasTranform.rect.height);
        Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
        puTranform.anchorMin = spawnPoint;
        puTranform.anchorMax = spawnPoint;
    }
}