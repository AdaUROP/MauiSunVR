using UnityEngine;
using System.Collections;

//[RequireComponent(typeof (Rigidbody))]
public class niceArc : MonoBehaviour {
    public Transform target;
    Vector3 v1;
    bool go;
    float timer = 0.1f;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
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
        /*
        v1 = transform.position;
        Vector3 vLook = (v1 + rb.velocity);
        transform.LookAt(vLook);
        */
        transform.LookAt(target);
    }

    public void startArc()
    {
        go = true;
    }

    public void stopArc()
    {
        go = false;
    }

    public void halt()

    {
        //something to freeze this position
    }

    void OnCollisionEnter(Collision col)
    {
        stopArc();
    }
}
