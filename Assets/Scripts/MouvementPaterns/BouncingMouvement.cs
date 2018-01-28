using UnityEngine;

public class BouncingMouvement : MonoBehaviour {

	private Rigidbody2D rb2D;

	[Tooltip("Vitesse du Ficher")]
	[Range(0, 15)]
	public float speed;

	private int direction;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		direction = Random.Range (0, 1);
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (direction == 1)
			rb2D.velocity = new Vector2(speed, Mathf.Lerp(0, -speed, 0.8f));
		else
			rb2D.velocity = new Vector2(-speed, Mathf.Lerp(0, -speed, 0.8f));
	}
		
    private void OnTriggerEnter2D(Collider2D colide)
    {
		if (direction == 1)
			direction = 0;
        else
			direction = 1;
    }
}