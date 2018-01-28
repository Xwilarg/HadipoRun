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
    [Tooltip("Interval before next popup spam spawn in seconds")]
	public Vector2 interval = new Vector2(10, 15);
    [Tooltip("Interval before next avest popup spawn in seconds")]
	public Vector2 inAvest = new Vector2(50, 70);
    [Tooltip("Parent containing popups")]
    public RectTransform canvas;
    [Tooltip("Avest popup")]
    public AvestNotificationController avest;
    [Tooltip("Max Strikes")]
    public uint maxStrikes = 3;
    [Tooltip("Immunity immediately after Strike.")]
    public float immunityLength = 5.0f;

    private float timer;
    private float refTimer;
    private float timerAvest;
    private float refTimerAvest;
    private GameObject downloadPopUp, alertPopUp, sys32Popup, infoPopUp, bigErrorPopUp, penichPopUp;
	private GameObject[] browsers = new GameObject[2];

    private string[] infos;


    private void ResetTimer()
    {
        timer = 0.0f;
        refTimer = Random.Range(interval.x, interval.y);
    }

    private void ResetTimerAvest()
    {
        timerAvest = 0.0f;
        refTimerAvest = Random.Range(inAvest.x, inAvest.y);
    }

	private void LoadPrefabs()
	{
		downloadPopUp = Resources.Load("Popup/DownloadPopup") as GameObject;
		alertPopUp = Resources.Load("Popup/AlertPopup") as GameObject;
		sys32Popup = Resources.Load("Popup/Sys32PopUp") as GameObject;
		infoPopUp = Resources.Load("Popup/InfoPopUp") as GameObject;
		bigErrorPopUp = Resources.Load("Popup/AlertPopUpBig") as GameObject;

		browsers[0] = Resources.Load("Ads/IEWindowPeniche") as GameObject;
		browsers[1] = Resources.Load("Ads/IEWindowMoumoute") as GameObject;
	}

    private void Start()
    {
        infos = File.ReadAllLines("NameDatabase/infos.dat");
        Assert.IsTrue(interval.x > 0 && interval.y > 0 && inAvest.x > 0 && inAvest.y > 0, "Intervalle bounds must be greater than 0.");
        Assert.IsTrue(interval.x < interval.y && inAvest.x < inAvest.y, "Intervalle lower bound must be lower than highter bound.");
		LoadPrefabs ();
        ResetTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
		if (timer > refTimer) {
			AddInfo ("Info", infos [Random.Range (0, infos.Length)]);
			ResetTimer();
		}
        timerAvest += Time.deltaTime;
        if (timerAvest > refTimerAvest)
        {
            avest.Show();
            ResetTimerAvest();
        }
    }

	public void AddDownload(string windowName, float fileSize)
	{
		GameObject go = InstanciateWindow (downloadPopUp, "Download Popup");
		go.GetComponent<DownloadController>().setDownloadVars(fileSize, windowName);
	}

	public void AddBrowser()
	{
		int nb = Random.Range(0, browsers.Length);
		InstanciateWindow (browsers[nb], "Browser " + nb);
	}

	public void AddInfo(string windowTitle, string description)
	{
		AddPopup (windowTitle, description, PopUpType.INFO);
	}

	public void AddBigError(string windowTitle, string description, string errors)
	{
		GameObject go = AddPopup (windowTitle, description, PopUpType.BIGERROR);
		go.GetComponent<BigErrorController> ().setErrors (errors);
	}

	public void AddAlert(string windowTitle, string description)
	{
		AddPopup (windowTitle, description, PopUpType.ALERT);
	}

	public void AddSys32(string windowTitle, string description)
	{
		AddPopup (windowTitle, description, PopUpType.SYS32);
	}

	public GameObject AddPopup(string windowTitle, string description, PopUpType type)
	{
		GameObject objectType = null;
		string name = "";
		switch (type)
		{
		case PopUpType.ALERT:
			objectType = alertPopUp; name = "alert"; break;
		case PopUpType.INFO:
			objectType = infoPopUp; name = "info"; break;
		case PopUpType.SYS32:
			objectType = sys32Popup; name = "sys32"; break;
		case PopUpType.BIGERROR:
			objectType = bigErrorPopUp; name = "bigerror"; break;
		default:
			return null;
		}
		GameObject go = InstanciateWindow (objectType, name);
		go.GetComponent<PopupController> ().setTitle (windowTitle).setDescription (description);
		return go;
	}

	private GameObject InstanciateWindow(GameObject type, string objectName = "anonymous")
	{
		GameObject newWindow = Instantiate(type, Vector3.zero, Quaternion.identity);
		newWindow.name = objectName;
		newWindow.transform.SetParent(canvas, false);
		RectTransform newWindowTranform = newWindow.transform as RectTransform;
		Vector2 minRatio = new Vector2((newWindowTranform.rect.width / 2) / canvas.rect.width, (newWindowTranform.rect.height / 2) / canvas.rect.height);
		Vector2 spawnPoint = new Vector2(Random.Range(minRatio.x, 1 - minRatio.x), Random.Range(minRatio.y, 1 - minRatio.y));
		newWindowTranform.anchorMin = spawnPoint;
		newWindowTranform.anchorMax = spawnPoint;
		return newWindow;
	}
}
