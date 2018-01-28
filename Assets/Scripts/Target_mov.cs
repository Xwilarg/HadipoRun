using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_mov : MonoBehaviour {
    [Tooltip("Gameobject to target")]
    public GameObject Target;
    [Tooltip("Speed"), Range(0, 15)]
    public float speed;
    [Tooltip("When re-target the player (min)")]
    public float minReTarget;
    [Tooltip("When re-target the player (max)")]
    public float maxReTarget;

    Rigidbody2D rb;
    float TargetTime;
    float currTime;

    // Use this for initialization
    void Start () {
        TargetTime = Random.Range(minReTarget, maxReTarget);
        rb = GetComponent<Rigidbody2D>();
        currTime = TargetTime;

        Debug.Log(rb);
        Debug.Log(TargetTime);
        Debug.Log(currTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (currTime <= 0)
        {
            float pursuitAngle = Mathf.Atan2(transform.position.y - Target.transform.position.y, transform.position.x - Target.transform.position.x);
            transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * (pursuitAngle + 48.8f));
            rb.AddForce(transform.up* -speed, ForceMode2D.Impulse);
            TargetTime = Random.Range(minReTarget, maxReTarget);
            currTime = TargetTime;
        }
        else
        {
            currTime -= Time.deltaTime;
            rb.AddForce(transform.forward * -speed, ForceMode2D.Impulse);
        }
    }
}
