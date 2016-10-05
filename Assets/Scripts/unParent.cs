using UnityEngine;
using System.Collections;

public class unParent : MonoBehaviour {

	// Use this for initialization
	void Start () {

        foreach(Transform obj in transform)
        {
            obj.transform.parent = null;
        }
        Destroy(gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
