using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rigChain : MonoBehaviour {
    //attach this script to the root joint in the hierarchy
    public float chainSize, offset, radius;
    public GameObject prefab, prefabModel, locator, dot, parentT;
    GameObject segment;
    public bool helix;
    public List<GameObject> objs;
    public List<GameObject> rbs;
    public List<GameObject> pts;
    List<ConfigurableJoint> cjs;
    ConfigurableJoint cjBase;
    float prefabX, prefabY, prefabZ;
    //GameObject root;
    Rigidbody connectRB;
    int breaks = 0;
    // Use this for initialization
    void Start ()
    {
        objs = new List<GameObject>();
        rbs = new List<GameObject>();
        pts = new List<GameObject>();
        //first we get the size of our prefab so we can properly place everything
        Debug.Log("Start");
        Renderer pMesh = prefabModel.GetComponent<Renderer>();

        prefabX = pMesh.bounds.size.x;
        prefabY = pMesh.bounds.size.y;
        prefabZ = pMesh.bounds.size.z;
        radius = prefabY/1.7f;
        offset = prefabX / 3;
        makeChain();
        //if (helix)
        //{
        //    makeHelix();
        //}
        //else makeChain();
        //setting a pause when finished so we can inspect it before continuing
        Debug.Break();
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.Space))
        {
            arrange();
        }
	
	}

    void makeChain()
    {
        GameObject current = null;
        GameObject first = null;
        GameObject segment;
        for (int i = 0; i < chainSize; i++)
        {
            segment = Instantiate(prefab, locator.transform.position, prefab.transform.rotation) as GameObject;
            objs.Add(segment);

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
        GameObject[] kill = { objs[0].gameObject, objs[1].gameObject };
        objs.RemoveAt(0);
        objs.RemoveAt(0);

        Destroy(kill[0].gameObject);
        Destroy(kill[1].gameObject);
  
        //current = objs[0];
        //objs.Remove(objs[0]);
        //Destroy(current.gameObject);

        breakThis();
        Debug.Break();

        if(helix)
        {
            arrange();
        }

    }

    void arrange()
    {
        Debug.Log(objs.Count);
        for(int i = 0; i < objs.Count - 2; i++)
        {
            if(i==0)
            {
                parentT.transform.position = objs[i+2].transform.position;
            }
            objs[i+2].transform.parent = parentT.transform;
        }
        Vector3 pos;
        float x, y, z;
        for(int i = 0; i < objs.Count; i++)
        {
            x = radius * Mathf.Cos(i);
            y = prefabY * i * offset *-1;
            z = radius * Mathf.Sin(i);
            pos = new Vector3(x, y-.5f, z);
            locator.transform.position = pos;
            pts.Add(Instantiate(dot, locator.transform.position, Quaternion.identity) as GameObject);
            if(pts.Count == 1)
            {
                parentT.transform.position = pts[0].transform.position;
                continue;
            }
            pts[i].transform.LookAt(pts[i - 1].transform);
            pts[i].transform.Rotate(90, 0, 0);
            
        }

        breakThis();
        

        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].transform.position = pts[i].transform.position;
            objs[i].transform.rotation = pts[i].transform.rotation;
        }

        breakThis();
        
    }

    void breakThis()
    {
        breaks++;
        Debug.Log("Brok! " + breaks);
        Debug.Break();
        
    }

}
