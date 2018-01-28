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
            string[] first = File.ReadAllLines("Assets/NameDatabase/first.dat");
            string[] second = File.ReadAllLines("Assets/NameDatabase/second.dat");
            string[] third = File.ReadAllLines("Assets/NameDatabase/third.dat");
            string[] extension = File.ReadAllLines("Assets/NameDatabase/extension.dat");
            textMesh.text = first[Random.Range(0, first.Length)] + '_' + second[Random.Range(0, second.Length)] + '_' + third[Random.Range(0, third.Length)] + '.' + extension[Random.Range(0, extension.Length)];
        }
        else
        {
            string[] fbi = File.ReadAllLines("Assets/NameDatabase/fbi.dat");
            textMesh.text = fbi[Random.Range(0, fbi.Length)] + ".rar";
        }
        sizeFile = Random.Range (sizeMin, sizeMax);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
