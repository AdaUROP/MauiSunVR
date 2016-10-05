using UnityEngine;
using System.Collections;

public class onTriggerSendMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            Debug.Log("That's a HIT");
            SendMessageUpwards("sunHit", c);
        }
    }
}
