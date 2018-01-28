using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.IO;

public enum PopUpType
{
    DOWNLOAD,
    ALERT,
    COMFIRM,
    INFO
}

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
    [Tooltip("Max Strikes")]
    public uint maxStrikes = 3;

    private float conspicuousity;
    private uint seededCount;
    [Tooltip("Conspicuousity grows by x * n, n being the number of seeded downloads.")]
    public float conspicuousityFactor;
    [Tooltip("Conspicuousity falls by x per seconds if no downloads are seeded.")]
    public float stealthFactor;

    private float timer;
    private float refTimer;
    private float timerAvest;
    private float refTimerAvest;
    private GameObject downloadPopUp, alertPopUp, comfirmPopUp, infoPopUp;
    private Text conspicuousityText;
    private string[] infos;
    private uint strikes;

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
        infos = File.ReadAllLines("Assets/NameDatabase/infos.dat");
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0 && inAvest.x > 0 && inAvest.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y && inAvest.x < inAvest.y, "Intervalle lower bound must be lower than highter bound.");
        downloadPopUp = Resources.Load("Popup/DownloadPopup") as GameObject;
        alertPopUp = Resources.Load("Popup/AlertPopup") as GameObject;
        comfirmPopUp = Resources.Load("Popup/ConfirmDeletePopUp") as GameObject;
        infoPopUp = Resources.Load("Popup/InfoPopUp") as GameObject;
        conspicuousityText = GameObject.Find("LeftCanvas").GetComponentInChildren<Text>();
        conspicuousity = 0.0f;
        seededCount = 0;
        strikes = 0;
        ResetTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
            GenericAdd(PopUpType.INFO, "Info", infos[Random.Range(0, infos.Length)]);
        if (seededCount == 0)
            conspicuousity -= stealthFactor * Time.deltaTime;
        else
            conspicuousity += conspicuousityFactor * seededCount * Time.deltaTime;
        conspicuousityText.text = System.String.Concat("Seeding: ", conspicuousity);
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

    private void getTracked(float deltaTime)
    {
        if (seededCount == 0)
            conspicuousity -= stealthFactor * Time.deltaTime;
        else
            conspicuousity += conspicuousityFactor * seededCount * Time.deltaTime;
        if (conspicuousity > 1000)
        {
            strikes++;
            conspicuousity = 0.0f;
        }
    }

    public void GenericAdd(PopUpType put, string windowName, string windowContent)
    {
        switch (put)
        {
            case PopUpType.ALERT:
                AddPopup(put, alertPopUp, "Error Popup", windowName, 0, windowContent);
                break;
            case PopUpType.COMFIRM:
                AddPopup(put, comfirmPopUp, "Confirm Popup", windowName, 0, windowContent);
                break;
            case PopUpType.INFO:
                AddPopup(put, infoPopUp, "Info Popup", windowName, 0, windowContent);
                break;
            default:
                Assert.IsTrue(false, "Invalid arguments");
                break;
        }
    }

    public void GenericAdd(PopUpType put, string windowName, float fileSize)
    {
        switch (put)
        {
            case PopUpType.DOWNLOAD:
                AddPopup(put, downloadPopUp, "Downloading Popup", windowName, fileSize);
                break;
            default:
                Assert.IsTrue(false, "Invalid arguments");
                break;
        }
    }

    private void AddAnnoyingPopup(string title, string content)
    {
        //AddPopup(samplePopUp, "Annoying Popup");
    }

    private void AddPopup(PopUpType pot, GameObject go, string popupName, string windowName, float fileSize = 0, string content = null)
    {
        GameObject pu = Instantiate(go, Vector3.zero, Quaternion.identity);
        pu.GetComponent<PopupScript>().setDownloadVars(fileSize, windowName, content);
        pu.name = popupName;
        pu.transform.SetParent(canvas, false);
        RectTransform puTranform = pu.transform as RectTransform;
        Vector2 minRatio = new Vector2((puTranform.rect.width / 2) / canvas.rect.width, (puTranform.rect.height / 2) / canvas.rect.height);
        Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
        string debugMsg = "Pu: " + puTranform.rect.width.ToString() + "; " + puTranform.rect.height.ToString() + "\n";
        debugMsg += "canvas: " + canvas.rect.width.ToString() + "; " + canvas.rect.height.ToString() + "\n";
        print(debugMsg);
        puTranform.anchorMin = spawnPoint;
        puTranform.anchorMax = spawnPoint;
        ResetTimer();
    }
}