﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour {
    
    [Tooltip("Loading bar if exist")]
    public Image loadingBar;
    [Tooltip("Cancel button if exist")]
    public Button cancelButton;
    [Tooltip("Window name")]
    public Text windowName;
    [Tooltip("Window content (text)")]
    public Text windowContent;
    [Tooltip("If download popup, download informations")]
    public Text downloadInfos;

    private float currTime;
    private float loadingTime = 0;
    private float fileSize = 0;
    private ScoreManager sm;
    public string fileName { set; private get; }
    private const float downloadSpeed = 98;
    private float downloaded;

    public void setDownloadVars(float fileSize, string windowName)
    {
        this.fileSize = fileSize;
        this.windowName.text = windowName;
    }

    public void setDownloadInfos()
    {
        if (downloadInfos != null)
        {
            downloadInfos.text = "Downloaded:	" + (downloaded / 1000).ToString("0.0") + " MB in " + currTime.ToString("0.0") + " seconds" + System.Environment.NewLine +
                                 "Download to:	C:\\Users\\Kevin-du-84\\Music" + System.Environment.NewLine +
                                 "Transfer rate: " + (downloadSpeed + Random.Range(-0.2f, 0.2f)).ToString("0.0") + " KB/s" + System.Environment.NewLine;
        }
        else
            Assert.IsNotNull(downloadInfos);
    }

    private void Start()
    {
        downloaded = 0.0f;
        loadingTime = fileSize / downloadSpeed;
        currTime = 0.0f;
        setDownloadInfos();
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    private void Update ()
    {
        currTime += Time.deltaTime;
        if (loadingBar != null && Random.Range(0, 100) < 5)
        {
            downloaded += downloadSpeed * Time.deltaTime;
            float prog = currTime / loadingTime;
            if (prog > 1.0f)
            {
                cancelButton.GetComponentInChildren<Text>().text = "Ok";
                Button b = cancelButton.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(Accept);
                loadingBar.rectTransform.localScale = new Vector2(1.0f, loadingBar.rectTransform.localScale.y);
                loadingBar = null;
            }
            else
                loadingBar.rectTransform.localScale = new Vector2(prog, loadingBar.rectTransform.localScale.y);
            setDownloadInfos();
        }
	}

    public void Accept()
    {
        sm.improveScore(fileSize);
        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}