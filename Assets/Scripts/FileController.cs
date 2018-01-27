using UnityEngine;
using System.Configuration;

public class FileController : MonoBehaviour {

	[Tooltip("Vitesse du Ficher")]
	[Range(0, 15)]
	public float speed;
	[Tooltip("Mouvement horizontal du Ficher")]
	[Range(1, -1)]
	public float move_horizontal;
	[Tooltip("Mouvement vertical du Ficher")]
	[Range(0, -1)]
	public float move_vertical;
	public float size {set; private get;}
	public string title { set; private get;}

	private Rigidbody2D rb;
	private TextMesh textMesh;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		textMesh = GetComponentInChildren<TextMesh> ();
		title = "audio.mp3";
		textMesh.text = title;
		size = Random.Range (1, 7);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector2 movement = new Vector2 (move_horizontal, move_vertical);
		rb.velocity = new Vector2(Mathf.Lerp(0, move_horizontal * speed, 0.8f), Mathf.Lerp(0, move_vertical * speed, 0.8f));
		rb.AddForce (movement * speed);
	}
}
