using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour {

    [Tooltip("ScoreManager attached to the GameManager")]
    ScoreManager sm;
    [Tooltip("Loading bar if exist")]
    public Image loadingBar;
    [Tooltip("Cancel button if exist")]
    public Button cancelButton;
    [Tooltip("Accept button if exist")]
    public Button acceptButton;

    private float currTime;
    public float loadingTime { set; private get; }
    public int fileSize { set; private get; }

    private void Start()
    {
        currTime = 0.0f;
        loadingTime = -1.0f;
        fileSize = 0;
    }

    private void Update ()
    {
        Assert.IsTrue(fileSize > 0, "File Size must be set as a positive value before first Update() call");
        if (loadingBar != null)
        {
            Assert.IsTrue(loadingTime > 0.0f, "Loading time must be set as a positive value before first Update() call");
            currTime += Time.deltaTime;
            float prog = currTime / loadingTime;
            if (prog > 1.0f)
            {
                acceptButton.interactable = true;
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
