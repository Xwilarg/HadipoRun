using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.IO;

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
            string[] first = File.ReadAllLines("NameDatabase/first.dat");
            string[] second = File.ReadAllLines("NameDatabase/second.dat");
            string[] third = File.ReadAllLines("NameDatabase/third.dat");
            string[] extension = File.ReadAllLines("NameDatabase/extension.dat");
            string finalText = first[Random.Range(0, first.Length)] + '_' + second[Random.Range(0, second.Length)] + '_' + third[Random.Range(0, third.Length)] + '.' + extension[Random.Range(0, extension.Length)];
            textMesh.text = finalText;
            title = finalText;
        }
        else
        {
            string[] fbi = File.ReadAllLines("NameDatabase/fbi.dat");
            string finalText = fbi[Random.Range(0, fbi.Length)] + ".rar";
            textMesh.text = finalText;
            title = finalText;
        }
        sizeFile = Random.Range (sizeMin, sizeMax);
	}
}
