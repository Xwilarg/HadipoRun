using UnityEngine;
using UnityEngine.UI;

public class DownloadController : MonoBehaviour {

	[Tooltip("Loading bar if exist")]
	public Image loadingBar;
	[Tooltip("Cancel button if exist")]
	public Button cancelButton;
	[Tooltip("Cross button if exist")]
	public Button crossButton;
	[Tooltip("Window name")]
	public Text windowName;
	[Tooltip("Window content (text)")]
	public Text windowContent;
	[Tooltip("If download popup, download informations")]
	public Text downloadInfos;
	[Tooltip("If big error popup, detailled informations")]
	public Text bigErrorInfos;
    [Tooltip("Time in sec before starting to trigger Hadipo")]
    [Range(0.1f, 10.0f)]
    public float hadipoRef;

	private float currTime;
	private float loadingTime = 0;
	private float fileSize = 0;
	public string fileName { set; private get; }
	private const float downloadSpeed = 1000;
	private float downloaded;
	private ScoreManager scoreManager = null;
    private float currHadipo;

	public void setDownloadVars(float fileSize, string windowName)
	{
		this.fileSize = fileSize;
		this.windowName.text = windowName;
	}

	public void setDownloadInfos()
	{
		if (downloadInfos != null)
		{
			downloadInfos.text = "Downloaded:	" + (downloaded / 1000.0f).ToString("0.0") + " MB in " + currTime.ToString("0.0") + " seconds" + System.Environment.NewLine +
				"Download to:	C:\\Users\\Kevin-du-84\\Music" + System.Environment.NewLine +
				"Transfer rate: " + (downloadSpeed + Random.Range(-0.2f, 0.2f)).ToString("0.0") + " KB/s" + System.Environment.NewLine;
		}
	}

	private void Start()
	{
		downloaded = 0.0f;
		loadingTime = fileSize / downloadSpeed;
		currTime = 0.0f;
		setDownloadInfos();
        currHadipo = 0.0f;
	}

	private void Update ()
	{
		currTime += Time.deltaTime;
		downloaded += downloadSpeed * Time.deltaTime;
		if (loadingBar != null && Random.Range(0, 100) < 5)
		{
			float prog = currTime / loadingTime;
			if (prog > 1.0f)
			{
				cancelButton.GetComponentInChildren<Text>().text = "Ok";
				Button b = cancelButton.GetComponent<Button>();
				b.onClick.RemoveAllListeners();
				b.onClick.AddListener(Accept);
				Button cross = crossButton.GetComponent<Button>();
				cross.onClick.RemoveAllListeners();
				cross.onClick.AddListener(Accept);
				loadingBar.rectTransform.localScale = new Vector2(1.0f, loadingBar.rectTransform.localScale.y);
				loadingBar = null;
			}
			else
				loadingBar.rectTransform.localScale = new Vector2(prog, loadingBar.rectTransform.localScale.y);
			setDownloadInfos();
		}
        else if (loadingBar == null)
        {
            currHadipo += Time.deltaTime;
            if (currHadipo > hadipoRef)
            {
                if (scoreManager == null)
                    scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
                scoreManager.increaseHadipoScore(Time.deltaTime);
            }
        }
	}

	public void Accept()
	{
		ScoreManager sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
		sm.improveScore(fileSize);
		Destroy(gameObject);
	}

	public void Cancel()
	{
		Destroy(gameObject);
	}

	public void EndGame()
	{
		ScoreManager sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
		sm.gameOverBSOD();
	}
}
