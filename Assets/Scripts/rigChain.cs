using UnityEngine;
using System.Collections;


public class rigChain : MonoBehaviour {
    //attach this script to the root joint in the hierarchy
    public float chainSize;
    public GameObject prefab, locator;
    public bool helix;

    //ConfigurableJoint cjBase;
    float prefabX, prefabY, prefabZ;
    //GameObject root;
    Rigidbody connectRB;
    // Use this for initialization
    void Start ()
    {
        //first we get the size of our prefab so we can properly place everything
        Debug.Log("Start");
        Renderer pMesh = prefab.GetComponent<Renderer>();
        
        prefabX = pMesh.bounds.size.x;
        prefabY = pMesh.bounds.size.y;
        prefabZ = pMesh.bounds.size.z;
        
        if(helix)
        {
            makeCoil();
        }
        else makeChain();
        //setting a pause when finished so we can inspect it before continuing
        Debug.Break();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void makeChain()
    {
        GameObject current = null;
        GameObject first = null;
        GameObject segment;
        ConfigurableJoint cjBase;
        for (int i = 0; i < chainSize; i++)
        {
            Debug.Log("rope " + i);
            segment = Instantiate(prefab, locator.transform.position, prefab.transform.rotation) as GameObject;
            cjBase = segment.AddComponent<ConfigurableJoint>();

            cjBase.xMotion = cjBase.yMotion = cjBase.zMotion = ConfigurableJointMotion.Locked;
            cjBase.angularYMotion = ConfigurableJointMotion.Limited;
            cjBase.angularXMotion = cjBase.angularZMotion = ConfigurableJointMotion.Free;
            cjBase.enablePreprocessing = false;
            cjBase.projectionMode = JointProjectionMode.PositionAndRotation;

            if (current == null)
            {
                first = segment;
                current = segment;
                continue;
                
            }

            cjBase.connectedBody = current.GetComponent<Rigidbody>();
            locator.transform.position -= new Vector3(0, prefabY, 0);
            current = segment;
        }
        cjBase = first.GetComponent<ConfigurableJoint>();
        Destroy(cjBase);
    }

    public void makeCoil()
    {
        GameObject current = null;
        GameObject first = null;
        GameObject segment;
        ConfigurableJoint cjBase;
        float x, y, z;
        Vector3 pos;

        for (int i = 1; i < chainSize+1; i++)
        {
            x = Mathf.Cos(i);
            y = Mathf.Sin(i);
            z = prefabZ*i;
            pos = new Vector3(x, y, z); 
            Debug.Log("rope " + i);
            segment = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
            cjBase = segment.AddComponent<ConfigurableJoint>();

            cjBase.xMotion = cjBase.yMotion = cjBase.zMotion = ConfigurableJointMotion.Locked;
            cjBase.angularYMotion = ConfigurableJointMotion.Limited;
            cjBase.angularXMotion = cjBase.angularZMotion = ConfigurableJointMotion.Free;
            cjBase.enablePreprocessing = false;
            cjBase.projectionMode = JointProjectionMode.PositionAndRotation;

            if (current == null)
            {
                first = segment;
                current = segment;
                continue;

            }

            cjBase.connectedBody = current.GetComponent<Rigidbody>();
            locator.transform.position = pos;
            current = segment;
        }
        cjBase = first.GetComponent<ConfigurableJoint>();
        Destroy(cjBase);

    }
}
