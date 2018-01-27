using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour {
    
    [Tooltip("Loading bar if exist")]
    public Image loadingBar;
    [Tooltip("Cancel button if exist")]
    public Button cancelButton;

    private float currTime;
    private float loadingTime = -1;
    private int fileSize = -1;
    private ScoreManager sm;

    public void setDownloadVars(float loadingTime, int fileSize)
    {
        this.loadingTime = loadingTime;
        this.fileSize = fileSize;
    }

    private void Start()
    {
        currTime = 0.0f;
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    private void Update ()
    {
        if (loadingBar != null)
        {
            Assert.IsTrue(loadingTime > 0.0f && fileSize > 0, "Loading time and file size must be set as a positive value before first Update() call");
            currTime += Time.deltaTime;
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
