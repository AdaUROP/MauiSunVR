using UnityEngine;
using System.Collections;

public class changeMatRender : MonoBehaviour {
    public Material mat, mat2, mat3, mat4, mat5;
    public int val;
    public bool print;
	// Use this for initialization
	void Start () {
        //mat = GetComponent<Material>() as Material;

        mat.renderQueue = val;
        mat2.renderQueue = val;
        mat3.renderQueue = val;
        mat4.renderQueue = val;
        mat5.renderQueue = val;
        if (print)
        {
            Debug.Log(mat.renderQueue + " This is unity's lack of documentation");
        }
	}
	
	// Update is called once per frame
	void Update () {
        /*
        mat.renderQueue = val;
        mat2.renderQueue = val;
        */
    }

    void OnPreRender()
    {
        /*
        mat.renderQueue = val;
        mat2.renderQueue = val;
        mat3.renderQueue = val;
        mat4.renderQueue = val;
        */
    }
}
