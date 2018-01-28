using UnityEngine;

public class VerticalMouvement : MonoBehaviour {

	private Rigidbody2D rb2D;

	[Tooltip("Vitesse du Ficher")]
	[Range(0, 15)]
	public float speed;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb2D.velocity = new Vector2(0.0f, Mathf.Lerp(0, -speed, 0.8f));
	}
}
