using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PopUpManager : MonoBehaviour
{

    [Tooltip("Intervalle before next popup spam spawn in seconds")]
    public Vector2 intervalle;
    [Tooltip("Intervalle before next avest popup spawn in seconds")]
    public Vector2 inAvest;
    [Tooltip("Parent containing popups")]
    public RectTransform canvas;
    [Tooltip("Avest popup")]
    public AvestNotificationController avest;

    private float conspicuousity;
    private uint seededCount;
    [Tooltip("Conspicuousity grows by pow(cons, n), n being the number of seeded downloads.")]
    public float conspicuousityFactor;
    [Tooltip("Conspicuousity falls by x per seconds if no downloads are seeded.")]
    public float stealthFactor;

    private float timer;
    private float refTimer;
    private float timerAvest;
    private float refTimerAvest;
    private GameObject samplePopUp;
    private Text conspicuousityText;

    private void ResetTimer()
    {
        timer = 0.0f;
        refTimer = Random.Range(intervalle.x, intervalle.y);
    }

    private void ResetTimerAvest()
    {
        timerAvest = 0.0f;
        refTimerAvest = Random.Range(inAvest.x, inAvest.y);
    }

    private void Start()
    {
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0 && inAvest.x > 0 && inAvest.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y && inAvest.x < inAvest.y, "Intervalle lower bound must be lower than highter bound.");
        samplePopUp = Resources.Load("Popup/DownloadPopup") as GameObject;
        conspicuousityText = GameObject.Find("LeftCanvas").GetComponentInChildren<Text>();
        conspicuousity = 0.0f;
        seededCount = 0;
        ResetTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
        {
            AddAnnoyingPopup();
            ResetTimer();
        }
        if (seededCount == 0)
        {
            conspicuousity -= stealthFactor * timer;
            conspicuousityText.text = "Empty: ";
        }
        else
        {
            conspicuousity += Mathf.Pow(conspicuousityFactor, seededCount) * timer;
            conspicuousityText.text = "Seeding: ";
        }
        conspicuousityText.text = System.String.Concat(conspicuousityText.text, conspicuousity.ToString());
        timerAvest += Time.deltaTime;
        if (timerAvest > refTimerAvest)
        {
            avest.Show();
            ResetTimerAvest();
        }
    }

    public void sprout()
    {
        ++seededCount;
    }

    public void wither()
    {
        --seededCount;
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
        pu.transform.SetParent(canvas, false);
        RectTransform puTranform = pu.transform as RectTransform;
        Vector2 minRatio = new Vector2((puTranform.rect.width / 2) / canvas.rect.width, (puTranform.rect.height / 2) / canvas.rect.height);
        Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
        puTranform.anchorMin = spawnPoint;
        puTranform.anchorMax = spawnPoint;
    }
}