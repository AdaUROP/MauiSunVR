using UnityEngine;
using System.Collections;

public class ropeSegFix : MonoBehaviour {


    public float drag = 6;
    Collider col;
    Collider[] childCol;
    Rigidbody rb;
    MeshRenderer[] mr;
    MeshRenderer mainMR;
	// Use this for initialization
	void Start () {
        mr = GetComponentsInChildren<MeshRenderer>();
        mainMR = GetComponent<MeshRenderer>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnTriggerExit(Collider colli)
    {
        if(colli.CompareTag("areaLimit"))
        {
            if(!mainMR.enabled)
            {
                mainMR.enabled = true;
                col.enabled = true;
                for(int i = 0; i < mr.Length; i++)
                {
                    mr[i].enabled = true;
                }
            }
            //Destroy(col);
            rb.angularDrag = drag;
        }
    }
}
