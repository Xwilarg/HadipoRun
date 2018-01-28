using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.IO;

public enum PopUpType
{
    DOWNLOAD,
    ALERT,
    COMFIRM,
    INFO,
    BIGERROR,
    BROWSER,
    SYS32
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
    [Tooltip("Conspicuousity UI.")]
    public GameObject conspicuousityUI;
    [Tooltip("Conspicuousity after which you get a strike.")]
    public float maxConspicuousity = 1000.0f;
    [Tooltip("Max Strikes")]
    public uint maxStrikes = 3;
    [Tooltip("Immunity duration immediately after Strike.")]
    public float maxImmunity = 5.0f;

    [Tooltip("Conspicuousity grows by x * n, n being the number of seeded downloads.")]
    public float conspicuousityFactor = 1.3f;
    [Tooltip("Conspicuousity falls by x per seconds if no downloads are seeded.")]
    public float stealthFactor = 1.0f;
    private uint strikes;
    //private Sprite[] strikeSprites;
    private float strikeImmunity;
    private Slider conspicuousitySlider;
    private Text conspicuousityText;
    private Image conspicuousityFill;
    //private Image strikeUI;
    private float conspicuousity;
    private uint seededCount;

    private float timer;
    private float refTimer;
    private float timerAvest;
    private float refTimerAvest;
    private GameObject downloadPopUp, alertPopUp, comfirmPopUp, infoPopUp, bigErrorPopUp, penichPopUp, confirmDeletePopUp;
    private string[] infos;

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
        infos = File.ReadAllLines("NameDatabase/infos.dat");
        Assert.IsTrue(intervalle.x > 0 && intervalle.y > 0 && inAvest.x > 0 && inAvest.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(intervalle.x < intervalle.y && inAvest.x < inAvest.y, "Intervalle lower bound must be lower than highter bound.");
        downloadPopUp = Resources.Load("Popup/DownloadPopup") as GameObject;
        alertPopUp = Resources.Load("Popup/AlertPopup") as GameObject;
        comfirmPopUp = Resources.Load("Popup/ConfirmDeletePopUp") as GameObject;
        infoPopUp = Resources.Load("Popup/InfoPopUp") as GameObject;
        bigErrorPopUp = Resources.Load("Popup/AlertPopUpBig") as GameObject;
        confirmDeletePopUp = Resources.Load("Popup/ConfirmDeletePopUp") as GameObject;
        conspicuousityText = GameObject.Find("LeftCanvas").GetComponentInChildren<Text>();
        conspicuousitySlider = conspicuousityUI.GetComponentInChildren<Slider>();
        strikes = 0;
        conspicuousity = 0.0f;
        strikeImmunity = 0.0f;
        seededCount = 0;
        conspicuousityFill = null;
        /*strikeSprites[0] = Resources.Load<Sprite>("Sprite/WindowsIcon/SpyGreen");
        strikeSprites[1] = Resources.Load<Sprite>("Sprite/WindowsIcon/SpyYellow");
        strikeSprites[2] = Resources.Load<Sprite>("Sprite/WindowsIcon/SpyRed");*/
        Image[] imageComponents = conspicuousitySlider.GetComponentsInChildren<Image>();
        foreach (Image im in imageComponents)
        {
            if (im.name == "Fill")
            {
                conspicuousityFill = im;
                break;
            }
            print("possible image:" + im.name);
        }
        /*strikeUI = conspicuousityUI.GetComponentInChildren<Image>();
        strikeUI.sprite = strikeSprites[strikes];*/
        ResetTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > refTimer)
            GenericAdd(PopUpType.INFO, "Info", infos[Random.Range(0, infos.Length)]);
        conspicuousityText.text = string.Concat("Strikes: ", strikes);
        getTracked(Time.deltaTime);
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
        print("Getting Tracked");
        conspicuousitySlider.maxValue = (strikeImmunity > 0.0f) ? maxImmunity : maxConspicuousity;
        conspicuousitySlider.value = (strikeImmunity > 0.0f) ? strikeImmunity : conspicuousity;
        if (strikeImmunity > 0.0f)
        {
            strikeImmunity -= deltaTime;
            conspicuousityFill.color = (Mathf.Repeat(strikeImmunity, .5f) < .25f) ? Color.yellow : Color.magenta;
        }
        else
        {
            conspicuousityFill.color = new Color(1.0f, 0.7f, 0.0f, 1.0f);
            conspicuousity += deltaTime * ((seededCount == 0) ? -stealthFactor : conspicuousityFactor * seededCount);
        }
        print("Strikes: " + strikes + " and conspicuousity: " + conspicuousity.ToString());
        if ((conspicuousity > 1000) && (strikes < maxStrikes))
        {
            strikes++;
            //strikeUI.sprite = strikeSprites[strikes];
            conspicuousity = 0.0f;
            strikeImmunity = maxImmunity;
            if (strikes >= maxStrikes)
                gameOver();
            else
            {
                PopupScript[] popups = GameObject.FindObjectsOfType<PopupScript>();
                foreach (PopupScript popup in popups)
                    popup.Cancel();
            }
        }
    }

    public void GenericAdd(PopUpType put, string windowName = "", string windowContent = "", string additionalContent = "")
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
            case PopUpType.BIGERROR:
                AddPopup(put, bigErrorPopUp, "Big Error Popup", windowName, 0, windowContent, additionalContent);
                break;
            case PopUpType.BROWSER:
                int nb = Random.Range(0, 2);
                if (nb == 0)
                    penichPopUp = Resources.Load("Ads/IEWindowPeniche") as GameObject;
                else
                    penichPopUp = Resources.Load("Ads/IEWindowMoumoute") as GameObject;
                AddPopup(put, penichPopUp, "Browser Popup", windowName, 0, "");
                break;
            case PopUpType.SYS32:
                AddPopup(put, confirmDeletePopUp, "Delete Sys32 Popup", windowName, 0, windowContent);
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
		
    private void AddPopup(PopUpType pot, GameObject go, string popupName, string windowName, float fileSize = 0, string content = null, string additionalContent = null)
    {
        GameObject pu = Instantiate(go, Vector3.zero, Quaternion.identity);
        pu.GetComponent<PopupScript>().setDownloadVars(fileSize, windowName, content, additionalContent);
        pu.name = popupName;
        pu.transform.SetParent(canvas, false);
        RectTransform puTranform = pu.transform as RectTransform;
        Vector2 minRatio = new Vector2((puTranform.rect.width / 2) / canvas.rect.width, (puTranform.rect.height / 2) / canvas.rect.height);
        Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
        puTranform.anchorMin = spawnPoint;
        puTranform.anchorMax = spawnPoint;
        ResetTimer();
    }

   private void gameOver()
    {
        conspicuousityText.text = "Game Over!Game Over!Game Over!Game Over!";
    }
}