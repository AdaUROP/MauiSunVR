using UnityEngine;
using System.Collections;

public class respawnHook : MonoBehaviour {
    public GameObject prefab;
    GameObject current;
    public bool respawn;
    respawnHook rH;
    public ConfigurableJoint[] cj;
    SoftJointLimitSpring sjLS;
    SoftJointLimit sjL;
	// Use this for initialization
	void Start () {

        SoftJointLimitSpring sj = new SoftJointLimitSpring();
        sj.spring = 20000f;
        sj.damper = 5f;

        SoftJointLimit sjL = new SoftJointLimit();
        sjL.limit = .05f;
        sjL.bounciness = 0;
        sjL.contactDistance = .01f;

        current = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
        cj = current.GetComponentsInChildren<ConfigurableJoint>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (respawn)
        {
            Destroy(current);
            current = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
            cj = current.GetComponentsInChildren<ConfigurableJoint>();
            respawn = false;
            
        }
	}

    public void setSpring()
    {
        Debug.Log("springs");
        foreach(ConfigurableJoint cJoint in cj)
        {
            cJoint.yMotion = ConfigurableJointMotion.Limited;
                   
        }
    }

    public void noSpring()
    {
        foreach(ConfigurableJoint cJoint in cj)
        {
            cJoint.yMotion = ConfigurableJointMotion.Locked;
        }
    }
}
