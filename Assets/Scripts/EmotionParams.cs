using UnityEngine;
using System.Collections;

public class EmotionParams {
	SunEmotion e;
	float time;

	public EmotionParams(SunEmotion e, float t) {
		this.e = e;
		this.time = t;
	}

	public SunEmotion getE() {
		return e;
	}

	public void setE(SunEmotion e) {
		this.e = e;
	}

	public float getTime() {
		return time;
	}

	public void setTime(float t) {
		this.time = t;
	}
}
