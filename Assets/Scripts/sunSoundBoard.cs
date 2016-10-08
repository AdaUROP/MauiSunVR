using UnityEngine;
using System.Collections;

public class sunSoundBoard : MonoBehaviour {
    public AudioClip[] sounds;
    AudioSource aud;
    bool laugh;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        laugh = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(laugh && !aud.isPlaying)
        {
            aud.PlayOneShot(sounds[(Random.Range(0, 5))]);
        }
	
	}

    public void setLaugh(bool val)
    {
        bool laugh = val;
    }
}
