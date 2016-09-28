using UnityEngine;
using System.Collections;


public class rigChain : MonoBehaviour {
    //attach this script to the root joint in the hierarchy
    public float sphereRadius, chainSize, prefabX, prefabY, prefabZ, x, y, z;
    public GameObject prefab, locator, root;
    Rigidbody connectRB;
    // Use this for initialization
    void Start ()
    {
        Debug.Log("Start");
        Renderer pMesh = prefab.GetComponent<Renderer>();
        /*
        prefabX = pMesh.bounds.size.x;
        prefabY = pMesh.bounds.size.y;
        prefabZ = pMesh.bounds.size.z;
        */
        //makeChain();
        traverse(gameObject);

        
        root.transform.parent = prefab.transform;
        Debug.Break();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void makeChain()
    {
        
        for(int i = 0; i < chainSize; i++)
        {
            
            GameObject segment = Instantiate(prefab, locator.transform.position, prefab.transform.rotation) as GameObject;

            HingeJoint hj = segment.GetComponent<HingeJoint>();
            
            hj.axis = new Vector3(0, 0, 1);
            if(i == 0)
            {
                connectRB = segment.GetComponent<Rigidbody>();
                Destroy(hj);
            }
            else
            {
                hj.connectedBody = connectRB;
                connectRB = segment.GetComponent<Rigidbody>();
            }
            locator.transform.Translate(prefabX * x * 1.01f, prefabY * y * 1.01f, prefabZ * z * 1.01f);
        }
    }


    //old recursive method in order to cycle through all possible children
    //of a given object no longer being used
    void traverse(GameObject obj)
    {
        //Debug.Log("Traverse");
        GameObject first = obj;
        Rigidbody firstRB = obj.GetComponent<Rigidbody>();
        foreach (Transform t in first.transform)
        {
            GameObject second = t.gameObject;
            
            second.AddComponent<Rigidbody>();
            
            Rigidbody secondRB = second.GetComponent<Rigidbody>();
            secondRB.constraints = RigidbodyConstraints.FreezeRotationY;
            second.AddComponent<SphereCollider>();
            SphereCollider col = second.GetComponent<SphereCollider>();
            col.radius = sphereRadius;

            second.AddComponent<SpringJoint>();
            SpringJoint sj = second.GetComponent<SpringJoint>();
            sj.connectedBody = firstRB;
            sj.enablePreprocessing = false;
            sj.spring = 100000;
            //secondRB.constraints = RigidbodyConstraints.FreezeRotationY;
            //secondRB.drag = 1;
            //secondRB.mass = 5;
            second.AddComponent<HingeJoint>();
            HingeJoint hj = second.GetComponent<HingeJoint>();
            hj.connectedBody = firstRB;
            hj.enablePreprocessing = false;
            //second.transform.parent = gameObject.transform;
            traverse(t.gameObject);
        }
    }
}
