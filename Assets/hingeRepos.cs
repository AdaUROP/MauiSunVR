using UnityEngine;
using System.Collections;

public class hingeRepos : MonoBehaviour {

	public GameObject hingeGoTo;
	public float lerpval = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (hingeGoTo.transform);
		transform.position = Vector3.Lerp (hingeGoTo.transform.position, transform.position, lerpval);
	}
}
