using UnityEngine;
using System.Collections;

public class dayTime : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyUp("space"))
        {
            anim.Play("4sec");
        }

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("4sec"))
        {
            anim.Play("New State");
        }
	}
}
