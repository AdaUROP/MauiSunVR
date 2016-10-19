using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class envParticles_Sound : MonoBehaviour {

    public GameObject cloudFX, system2, system3, audPos;
    public AudioClip boop, heavyBoop, softBoop, splash, hit1, hit2, hit3, bite;
    AudioSource aud, audPoint;
    public AudioSource[] muzakSystem;
    List <AudioClip> clips;
    Random rand;
	// Use this for initialization
	void Start () {
        audPoint = audPos.GetComponent<AudioSource>();
        clips = new List<AudioClip>();
        clips.Add(boop);
        clips.Add(heavyBoop);
        clips.Add(softBoop);
        clips.Add(hit1);
        clips.Add(hit2);
        clips.Add(hit3);
        clips.Add(bite);
	
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

    public void playBite(Transform t)
    {
        audPos.transform.position = t.position;
        audPoint.PlayOneShot(clips[6]);
    }

    public void startSong()
    {
        muzakSystem[1].mute = true;
        muzakSystem[0].mute = false;
    }

    public void fightSong()
    {
        muzakSystem[0].mute = true;
        muzakSystem[1].mute = false;
        muzakSystem[1].Play();

    }

    public void endSong()
    {
        
    }


}
