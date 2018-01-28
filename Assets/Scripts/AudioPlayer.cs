using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    AudioSource player;
    AudioClip Aclip;

	// Use this for initialization
	void Start () {
        player = GetComponent<AudioSource>();
        Aclip = new AudioClip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play(string name)
    {
        if (File.Exists("Assets/Sounds/" + name))
        {
            Aclip = (AudioClip)Resources.Load("Sounds/" + name);
            player.clip = Aclip;
            player.Play();
            Aclip.UnloadAudioData();
        }
    }
}
