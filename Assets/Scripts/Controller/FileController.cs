using UnityEngine;
using System.Configuration;
using System.IO;

public class FileController : MonoBehaviour {

	[Tooltip("Vitesse du Ficher")]
	[Range(0, 15)]
	public float speed;
    [Tooltip("Change of being a virus in %.")]
    [Range(0, 100)]
    public int chanceVirus;

	public float size {private set; get;}
	public string title;
	private Rigidbody2D rb;
	private TextMesh textMesh;
    private bool isVirus;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		textMesh = GetComponentInChildren<TextMesh> ();
        isVirus = (Random.Range(0, 100) < chanceVirus);
        if (isVirus)
        {
            string[] first = File.ReadAllLines("Assets/NameDatabase/virus.dat");
            string[] extension = File.ReadAllLines("Assets/NameDatabase/extensionVirus.dat");
            title = first[Random.Range(0, first.Length)] + '.' + extension[Random.Range(0, extension.Length)];
        }
        else
        {
            string[] first = File.ReadAllLines("Assets/NameDatabase/first.dat");
            string[] second = File.ReadAllLines("Assets/NameDatabase/second.dat");
            string[] third = File.ReadAllLines("Assets/NameDatabase/third.dat");
            string[] extension = File.ReadAllLines("Assets/NameDatabase/extension.dat");
            title = first[Random.Range(0, first.Length)] + '_' + second[Random.Range(0, second.Length)] + '_' + third[Random.Range(0, third.Length)] + '.' + extension[Random.Range(0, extension.Length)];
        }
		textMesh.text = title;
		size = Random.Range (1000, 3000);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb.velocity = new Vector2(0.0f, Mathf.Lerp(0, -speed, 0.8f));
	}
}
