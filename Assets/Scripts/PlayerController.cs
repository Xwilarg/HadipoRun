using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Tooltip("Vitesse du Joueur")]
	[Range(0, 15)]
	public float speed;
	
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();		
	}

	void FixedUpdate ()
	{
		float moveVertical = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2 (moveVertical, 0.0f);

		rb.AddForce (movement * speed);
	}
}
