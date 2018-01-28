
    [Tooltip("Parent containing popups")]
    public RectTransform canvas;
    [Tooltip("Avest popup")]
    public AvestNotificationController avest;
    
    [Tooltip("Canvas")]
    public Canvas canvas;

    private float conspicuousity;
    private uint seededCount;
    [Tooltip("Conspicuousity grows by pow(cons, n), n being the number of seeded downloads.")]
    public float conspicuousityFactor;
    [Tooltip("Conspicuousity falls by x per seconds if no downloads are seeded.")]
    public float stealthFactor;

        timerAvest += Time.deltaTime;
        if (timerAvest > refTimerAvest)
        {
            avest.Show();
            ResetTimerAvest();
        }
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
	}