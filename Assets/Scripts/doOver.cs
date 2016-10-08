using UnityEngine;
using System.Collections;

public class doOver : MonoBehaviour {

    bool leftArea;
    respawnHook rH;
	// Use this for initialization
	void Start ()
    {
         rH = GameObject.Find("ScriptBox").GetComponent<respawnHook>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void respawnCheck()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Water" && leftArea)
        {
            rH.respawn = true;
        }

        if(col.gameObject.name == "island" && leftArea)
        {
            rH.respawn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("areaLimit"))
        {
            leftArea = true;
        }
    }
}
