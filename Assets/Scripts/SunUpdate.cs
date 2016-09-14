using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false;
	int interval = 20;
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
				Color c = group.GetComponent<Renderer> ().material.color;
				if (c.g > 0 && d >= interval) {
					c.g--;
					d = 0;
				}
				group.GetComponent<Renderer> ().material.color = c;
				d++;
			}
		}
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
        }
    }


}
