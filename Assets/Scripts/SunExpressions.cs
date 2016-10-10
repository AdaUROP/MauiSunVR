using UnityEngine;
using System.Collections;

public class SunExpressions {
	public static SunEmotion mergeExpressions(SunEmotion one, SunEmotion two) {
		return new SunEmotion (
			Mathf.Min(one.getSmiling () + two.getSmiling (), 100f), 
			Mathf.Min(one.getAngryEyes () + two.getAngryEyes (), 100f),
			Mathf.Min(one.getSad () + two.getSad (), 100f), 
			Mathf.Min(one.getClosed () + two.getClosed (), 100f), 
			Mathf.Min(one.getPursed () + two.getPursed (), 100f),
			Mathf.Min(one.getAngry () + two.getAngry (), 100f)); 
	}

	public static readonly SunEmotion LIPS_AH = new SunEmotion (100f, 0f, 0f, 100f, 100f, 100f);
	public static readonly SunEmotion LIPS_EE = new SunEmotion(50f, 0f, 0f, 0f, 100f, 0f);
	public static readonly SunEmotion LIPS_EH = new SunEmotion (50f, 0f, 0f, 50f, 75f, 0f);
	public static readonly SunEmotion LIPS_O = new SunEmotion (0f, 0f, 0f, 0f, 100f, 0f);

	public static readonly SunEmotion LIPS_MBP = new SunEmotion (0f, 0f, 0f, 100f, 100f, 0f);
	public static readonly SunEmotion LIPS_SS = new SunEmotion(0f, 0f, 0f, 100f, 0f, 0f);
	public static readonly SunEmotion LIPS_FV = new SunEmotion (0f, 0f, 0f, 75f, 75f, 0f);
	public static readonly SunEmotion LIPS_DEF = new SunEmotion (0f, 0f, 0f, 50f, 75f, 0f);

	public static readonly SunEmotion EXPR_ANGRY = new SunEmotion(0f, 100f, 0f, 0f, 0f, 0f);
	public static readonly SunEmotion EXPR_SCARED = new SunEmotion(0f, 0f, 100f, 0f, 0f, 0f);
	public static readonly SunEmotion EXPR_DISGUSTED = new SunEmotion(0f, 50f, 50f, 0f, 0f, 0f);
}
