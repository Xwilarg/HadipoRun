using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Tooltip("Vitesse du Joueur")]
	[Range(0, 15)]
	public float speed;

	private Rigidbody2D rb;

    private PopUpManager popUp;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		popUp = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<PopUpManager> ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2 (moveHorizontal, 0.0f);
		rb.velocity = new Vector2(Mathf.Lerp(0, moveHorizontal * speed, 0.8f), 0.0f);
		rb.AddForce (movement * speed);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Item")) {
			FileDescription File = other.GetComponent<FileDescription> ();
			popUp.GenericAdd (PopUpType.DOWNLOAD, File.title, File.sizeFile);
            Destroy (other.gameObject);
        }
		if (other.gameObject.CompareTag ("Virus"))
        {
            for (int i = 0; i < 20; i++)
                popUp.GenericAdd(PopUpType.ALERT, "Error", "ふたなり-" + i + ".dll was not found.");
            Destroy (other.gameObject);
		}
    }
}