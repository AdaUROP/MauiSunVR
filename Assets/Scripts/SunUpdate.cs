using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false;
	int interval = 200;
	int d = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (collided) {
			GameObject group = GameObject.Find ("Group3");
			SkinnedMeshRenderer sunSmile = group.GetComponent<SkinnedMeshRenderer> ();
			if (sunSmile.GetBlendShapeWeight (0) <= 100) {
				sunSmile.SetBlendShapeWeight (0, sunSmile.GetBlendShapeWeight (0) + 2f);
				Color c = group.GetComponent<Renderer> ().material.color, destColor = group.GetComponent<Renderer> ().material.color;
				destColor.g /= 2;
				if (c.g > 0 && d <= interval) {
					Color.Lerp (c, destColor, d / interval);
					d++;
				}
			}
		}
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
			d = 0;
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
        }
    }


}
