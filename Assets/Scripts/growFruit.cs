using UnityEngine;
using System.Collections;

public class growFruit : MonoBehaviour {

    public float timer;
    public GameObject fruit;
    public Transform spawnLoc;
    bool spawn = false;
    float t;
    GameObject scriptB;

	// Use this for initialization
	void Start () {
        scriptB = GameObject.Find("ScriptBox");
        GameObject newFruit = Instantiate(fruit, spawnLoc.position, spawnLoc.rotation) as GameObject;
        newFruit.SendMessage("setGF", this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(spawn)
        {
            t -= Time.deltaTime;
        }
        if(t < 0)
        {
            t = timer;
            spawnFruit();
            spawn = false;
        }
	
	}

    void spawnFruit()
    {
        GameObject newFruit = Instantiate(fruit, spawnLoc.position, spawnLoc.rotation) as GameObject;
        newFruit.SendMessage("setGF", this);
        scriptB.SendMessage("emitCloud", gameObject.transform);
        scriptB.SendMessage("playBoop", gameObject.transform);
    }

    public void setSpawn()
    {
        spawn = true;
        t = timer;
    }
}