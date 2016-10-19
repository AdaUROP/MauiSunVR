using UnityEngine;
using System.Collections;

public class copyText : MonoBehaviour {
    public TextMesh tm;
    TextMesh thisTM; 
	// Use this for initialization
	void Start () {
        thisTM = GetComponent<TextMesh>();
	
	}
	
	// Update is called once per frame
	void Update () {

        thisTM.text = tm.text;
	
	}
}
