using UnityEngine;

public class VirusDescription : MonoBehaviour {

	public enum AlertLevel {
		LOW,
		MEDIUM,
		HIGH,
		HOLYSHIT
	}
	// Use this for initialization
	[Tooltip("Niveau de dangerosité du VIRUS")]
	public AlertLevel alertLevel;
	[Tooltip("Nom du Fichier")]
	public string title;

	private TextMesh textMesh;
	public float sizeFile{ private set; get;}

	void Start () {
		textMesh = GetComponentInChildren<TextMesh> ();
        string[] first = new string[] { "halfLife3", "autorun", "freeSteamKeys" };
        string[] extension = new string[] { "bat", "mp3.exe" };
		title = first[Random.Range(0, first.Length)] + '.' + extension[Random.Range(0, extension.Length)];
		textMesh.text = title;
	}
}
