using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour {

	Vector3 one;
	Vector3 two;
    bool grabbed;

    public GameObject wand;

    int deltaFrames = 10;
	int counter = 0;

	public Camera cam;

	// Use this for initialization
	void Start () {
		one = new Vector3 ();
		two = new Vector3 ();
		grabbed = false;
	}

	// Update is called once per frame
	void Update () {

        //Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y);
        
		if (grabbed) {

			if (!(one.x == 0 && one.x == 0 && one.z == 0) && counter == deltaFrames) two = new Vector3 (one.x, one.y, one.z);

			one = wand.transform.position;
			this.transform.position = new Vector3(one.x, one.y, one.z);
		} else if (!grabbed) {
            this.transform.forward = two - one;
            print(this.transform.forward);
            this.transform.GetComponent<Rigidbody> ().AddForce (new Vector3(this.transform.forward.x, this.transform.forward.y, 0) * Vector3.Magnitude(this.transform.forward));
		}
		if (counter == deltaFrames)
			counter = 0;
		else
			counter++;
    }

    public void setGrabbed(bool n)
    {
        grabbed = n;
    }
}
