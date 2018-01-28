using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour {
    
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

    private float currTime;
    private float loadingTime = 0;
    private float fileSize = 0;
    public string fileName { set; private get; }
    private const float downloadSpeed = 1000;
    private float downloaded;
    private PopUpManager pm;
    private bool seeding;
    public float seedingSince;

    public void setDownloadVars(float fileSize, string windowName, string windowContent, string bigErrorInfos)
    {
        this.fileSize = fileSize;
        this.windowName.text = windowName;
        if (windowContent != null)
            this.windowContent.text = windowContent;
        else
            this.windowContent.text = windowName + this.windowContent.text;
        if (bigErrorInfos != null)
            this.bigErrorInfos.text = bigErrorInfos;
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
        pm = GameObject.Find("GameManager").GetComponent<PopUpManager>();
        seeding = false;
        seedingSince = 0.0f;
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
        if (((currTime / loadingTime) > 1.0f) && !seeding)
        {
            seedingSince += Time.deltaTime;
            if (seedingSince > 3.0f)
            {
                seeding = true;
                pm.sprout();
            }
        }
    }

    public void Accept()
    {
		ScoreManager sm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        sm.improveScore(fileSize);
        Destroy(gameObject);
        pm.wither();
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
