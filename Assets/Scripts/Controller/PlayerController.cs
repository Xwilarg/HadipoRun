using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Tooltip("Vitesse du Joueur")]
	[Range(0, 15)]
	public float speed;
    public string[] bigErrors;

	private Rigidbody2D rb;

    private PopUpManager popUp;
    
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		popUp = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<PopUpManager>();
        bigErrors = File.ReadAllLines("Assets/NameDatabase/errorsExplanations.dat");
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
            int virusType = Random.Range(0, 2);
            if (virusType == 0)
            {
                int max = Random.Range(3, 9);
                for (int i = 0; i < max; i++)
                    popUp.GenericAdd(PopUpType.ALERT, "Error", "ふたなり-" + i + ".dll was not found.");
            }
            else
            {
                string errors = "";
                for (int i = 0; i < 4; i++)
                    errors += bigErrors[Random.Range(0, bigErrors.Length)] + System.Environment.NewLine;
                popUp.GenericAdd(PopUpType.BIGERROR, "Fatal Error", "An error occured while executing the application", errors);
            }
            Destroy (other.gameObject);
		}
    }
}