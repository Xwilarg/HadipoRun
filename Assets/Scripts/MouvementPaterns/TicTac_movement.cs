using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTac_movement : MonoBehaviour {
    [Tooltip("Gameobject to target")]
    public GameObject Target;
    [Tooltip("Speed"), Range(0, 15)]
    public float speed;
    [Tooltip("Frequence")]
    public float frequency;

    float initialposition;
    Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        initialposition = Target.transform.position.x;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Round(gameObject.transform.position.x, 2) >= Math.Round(initialposition, 2))
        {
            Debug.Log("X (" + gameObject.transform.position.x + ") >= Pos (" + initialposition + ")");
            rb.velocity = new Vector2(-speed, Mathf.Lerp(0, -speed, 0.8f));
        }
        else if (Math.Round(gameObject.transform.position.x, 2) <= Math.Round(initialposition))
        {
            Debug.Log("X (" + gameObject.transform.position.x + ") <= Pos (" + initialposition + ")");
            rb.velocity = new Vector2(speed, Mathf.Lerp(0, -speed, 0.8f));
        }
        else if (Math.Round(gameObject.transform.position.x, 2) == Math.Round(initialposition, 2))
        {
            Debug.Log("Ancien point atteint");
            initialposition = Target.transform.position.x;
        }
    }
}
