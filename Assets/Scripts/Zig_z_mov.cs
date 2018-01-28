using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Zig_z_mov : MonoBehaviour {
    public float size { private set; get; }
    [Tooltip("Vitese de chute")]
    public float SpeedDet;
    [Tooltip("Chances d'être un virus")]
    public float Virchance;
    [Tooltip("Angle de changement")]
    public float Angle;

    bool right;
    bool isVirus;
    public float actualTime;
    string title;
    Rigidbody2D rb;
    TextMesh TextMesh;
    GameObject player;

	// Use this for initialization
	void Start () {
        right = true;
        rb = GetComponent<Rigidbody2D>();
        actualTime = 0;
        TextMesh = GetComponent<TextMesh>();
        isVirus = (Random.Range(0, 100) < Virchance);
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
        TextMesh.text = title;
        size = Random.Range(1000, 3000);
    }
	
	// Update is called once per frame
	void Update () {
        if (right)
            rb.velocity = new Vector2(SpeedDet, Mathf.Lerp(0, -SpeedDet, 0.8f));
        else
            rb.velocity = new Vector2(-SpeedDet, Mathf.Lerp(0, -SpeedDet, 0.8f));
    }

    private void OnTriggerEnter2D(Collider2D colide)
    {
        if (right)
            right = false;
        else
            right = true;
    }
}
