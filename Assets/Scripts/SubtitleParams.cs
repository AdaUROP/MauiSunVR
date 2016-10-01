using UnityEngine;
using System.Collections;

public class SubtitleParams {
	string text;
	int frames;

	public SubtitleParams(string t, int f) {
		text = t;
		frames = f;
	}

	public string getText() {
		return text;
	} 

	public int getFrames() {
		return frames;
	}

	public void setText(string t) {
		text = t;
	}

	public void setFrames(int f) {
		frames = f;	
	}
}
