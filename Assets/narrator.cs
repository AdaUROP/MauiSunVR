using UnityEngine;
using System.Collections;

public class narrator : MonoBehaviour {
    AudioSource aud;
    public AudioClip[] clips;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.Keypad1))
        {
            //Debug.Log("narration");
            if(!aud.isPlaying)
            {
                aud.PlayOneShot(clips[0]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[1]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[2]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[3]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[4]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[5]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(clips[6]);
            }
        }


    }
}
