using UnityEngine;
using UnityEngine.SocialPlatforms;

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
		textMesh = GetComponent<TextMesh> ();
		textMesh.text = title;
		sizeFile = Random.Range (sizeMin, sizeMax);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
