using UnityEngine;
using System.Collections;

public class eatMe : MonoBehaviour {

    public MeshFilter mf;
    public Mesh m1, m2, m3;
    public Material mat1, mat2, mat3;
    Renderer rend;
    int stage = 0;
	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyUp(KeyCode.B))
        {
            stage++;
            switch(stage)
            {
                case 1: oneBite();
                    break;
                case 2: twoBite();
                    break;
                case 3: threeBite();
                    break;
                case 4: finish();
                    break;
            }
        }
	}

    public void oneBite()
    {
        mf.mesh = m1;
        rend.material = mat1;
    }

    public void twoBite()
    {
        mf.mesh = m2;
        rend.material = mat2;
    }

    public void threeBite()
    {
        mf.mesh = m3;
        rend.material = mat3;
    }

    public void finish()
    {
        Destroy(gameObject);
    }
}
