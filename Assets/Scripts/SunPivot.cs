using UnityEngine;
using System.Collections;

public class SunPivot : MonoBehaviour {

	bool rotate = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.R)) {
			rotatePivot ();
		} else
			rotate = false;
		float ry = this.transform.rotation.y;
		if (Mathf.Abs(ry) >= (1f / 3f)) {
			rotate = false;
		}
		if (rotate) {
			if (Mathf.Abs (ry) < (1f/3f)) {
				if (ry == 0) {
					ry = Mathf.Floor (Random.Range (0f, 1.9f)) == 1 ? ry - (1f/300f) : ry + (1f/300f);
				} else if (ry < 0) {
					ry--;
				} else if (ry > 0) {
					ry++;
				}
			}
		} else {
			if (Mathf.Abs (ry) < (1f/3f)) {
				if (ry < 0) {
					ry += (1f/300f);
				} else if (ry > 0) {
					ry -= (1f/300f);
				}
			}
		}
		this.transform.rotation = new Quaternion (this.transform.rotation.x, ry, this.transform.rotation.z, this.transform.rotation.w);
	}

	public void rotatePivot() {
		rotate = true;
	}
}
