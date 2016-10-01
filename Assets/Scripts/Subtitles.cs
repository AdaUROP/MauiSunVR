using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class Subtitles : MonoBehaviour {

	public bool onOff; // Toggle subtitles

	string text; // Subtitle text
	bool display = false;
	int frames = 0;
	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh> ();
		textMesh.text = "";

	}

	// Sets subtitle text
	public void displayScript(SubtitleParams ps) {
		display = true;
		this.text = ps.getText();
		this.frames = ps.getFrames();
	}

	// Update is called once per frame
	void Update () {
		if (onOff) {
			if (display) {
				textMesh.text = this.text;
				frames--;
				if (frames == 0) { // If audio is playing
					display = false;
				}
			} else {
				textMesh.text = "";
			}
		}
	}
}
