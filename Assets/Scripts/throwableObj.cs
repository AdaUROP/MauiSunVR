using UnityEngine;
using System.Collections;

public class throwableObj : MonoBehaviour {
    Collider col;
    bool active = false;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setReady()
    {
        active = true;
        gameObject.tag = "throwable";
    }
    public void disable()
    {
        active = false;
        gameObject.tag = "Untagged";
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("hand") && !active)
        {
            setReady();
        }
    }

}
