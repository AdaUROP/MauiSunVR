using UnityEngine;
using System.Collections;

public class imAFruit : MonoBehaviour {
    growFruit gf;
    throwableObj me;
	// Use this for initialization
	void Start () {
        me = GetComponent<throwableObj>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if(me.grabbed)
        {
            gf.SendMessage("setSpawn");
        }
	
	}

   public void setGF(growFruit thisThing)
    {
        gf = thisThing;
    }
}
