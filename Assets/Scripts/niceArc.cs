using UnityEngine;
using System.Collections;

//[RequireComponent(typeof (Rigidbody))]
public class niceArc : MonoBehaviour {
    
    Vector3 v1, v2;
    bool go;
    float timer = 0.1f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(go)
        {
            arc();
        }
	
	}

    void arc()
    {
        if(timer<0)
        {
            v2 = transform.position;
            Vector3 vLook = (v2 - v1) + v2;
            transform.LookAt(vLook);
            v1 = transform.position;
            timer = .1f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void startArc()
    {
        go = true;
        v1 = transform.position;
    }

    public void halt()
    {
        //something to freeze this position
    }
}
