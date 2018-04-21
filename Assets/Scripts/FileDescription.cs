using UnityEngine;

public class FileDescription : MonoBehaviour {

	// Use this for initialization
	[Tooltip("Taille Max du ficher")]
	public float sizeMax;
	[Tooltip("Taille Min du ficher")]
	public float sizeMin;
	[Tooltip("Nom du Fichier")]
	public string title;

	private TextMesh textMesh;
	public float sizeFile{ private set; get;}

	void Start () {
		textMesh = GetComponentInChildren<TextMesh> ();
        if (title == "Audio")
        {
            string[] first = new string[] { "midget", "big", "blue", "spooky", "milky", "ero" };
            string[] second = new string[] { "spider", "cannister", "nuclearPowerPlant", "calamar", "Moumouth", "GGJ" };
            string[] third = new string[] { "porn", "music", "HD", "live", "instru", "LQ" };
            string[] extension = new string[] { "mp3", "avi", "ogg" };
            string finalText = first[Random.Range(0, first.Length)] + '_' + second[Random.Range(0, second.Length)] + '_' + third[Random.Range(0, third.Length)] + '.' + extension[Random.Range(0, extension.Length)];
            textMesh.text = finalText;
            title = finalText;
        }
        else
        {
            string[] fbi = new string[] { "nuclearLaunchCode", "hl3SourceCode", "VincentNudes" };
            string finalText = fbi[Random.Range(0, fbi.Length)] + ".rar";
            textMesh.text = finalText;
            title = finalText;
        }
        sizeFile = Random.Range (sizeMin, sizeMax);
	}
}
