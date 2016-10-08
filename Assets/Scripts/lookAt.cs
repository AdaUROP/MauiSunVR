using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {
    public Transform target;
    public bool lateU = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!lateU)
        {
            transform.LookAt(target);
        }
	}

    void LateUpdate()
    {
        if(lateU)
        {
            transform.LookAt(target);
        }
    }
}
