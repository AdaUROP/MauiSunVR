using UnityEngine;
using System.Collections;

public class sunSoundBoard : MonoBehaviour {
    public AudioClip[] sounds;
    AudioSource aud;
    bool laugh;
    float timer, tCheck;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        laugh = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(laugh && !aud.isPlaying && tCheck >= timer)
        {   
            aud.PlayOneShot(sounds[(Random.Range(0, 5))]);
            tCheck = 0;
            setTimer(Random.Range(.5f, 2f));
        }

        tCheck += Time.deltaTime;

	
	}

    public void setLaugh(bool val)
    {
        laugh = val;
    }

    public void setTimer(float value)
    {
        timer = value;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("water"))
        {
            laugh = !laugh;
        }
    }
}
