using UnityEngine;
using System.Collections;

public class wToGo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			transform.position = transform.position + transform.forward * 0.05f;
		}
	}
}
