using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	int interval = 20;
	int d = 0;
	int timesCollided = 0;
	bool updated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject group = GameObject.Find ("Group3");
		SkinnedMeshRenderer sunSmile = group.GetComponent<SkinnedMeshRenderer> ();
		Material m = group.GetComponent<Renderer> ().material;
		Color c = m.color;
		if (timesCollided <= 2 && timesCollided > 0) {
			if (sunSmile.GetBlendShapeWeight (0) < 50 * timesCollided) {
				sunSmile.SetBlendShapeWeight (0, sunSmile.GetBlendShapeWeight (0) + 2f);
			}
			if (Mathf.Floor(c.g * 255f) >= (2 - timesCollided) * 91) {
				c.g -= (1f/255f);
				m.SetColor ("_Color", c);
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "throwable" && !updated) {
			timesCollided++;
			updated = true;
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.gameObject.tag == "throwable" && updated) {
			updated = false;
		} 
	}
}
