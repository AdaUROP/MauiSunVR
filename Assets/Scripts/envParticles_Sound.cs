using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class envParticles_Sound : MonoBehaviour {

    public GameObject cloudFX, system2, system3;
    public AudioClip boop, heavyBoop, softBoop, splash, hit1, hit2, hit3;
    AudioSource aud;
    List <AudioClip> clips;
    Random rand;
	// Use this for initialization
	void Start () {
        clips = new List<AudioClip>();
        clips.Add(boop);
        clips.Add(heavyBoop);
        clips.Add(softBoop);
        clips.Add(hit1);
        clips.Add(hit2);
        clips.Add(hit3);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void emitCloudwSound(Transform t)
    {
        GameObject newFX = Instantiate(cloudFX, t.position, Quaternion.identity) as GameObject;
        aud = newFX.GetComponent<AudioSource>();
        int i = Random.Range(0, 2);
        aud.PlayOneShot(clips[i+4]);
        //Debug.Log("playing clip" + i);
        Destroy(newFX, 5f);

    }

    public void emitCloud(Transform t)
    {
        GameObject newFX = Instantiate(cloudFX, t.position, Quaternion.identity) as GameObject;
        aud = newFX.GetComponent<AudioSource>();
        Destroy(newFX, 1f);
    }

    public void playBoop(Transform t)
    {
        int i = Random.Range(0, 2);
        aud.PlayOneShot(clips[i]);
    }
}
