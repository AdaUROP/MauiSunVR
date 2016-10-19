using UnityEngine;
using System.Collections;

public class lookAtMePlease : MonoBehaviour {

    public GameObject you, me;
    public float angle;
    public GameObject adjust;
    Quaternion savePos;
    public SteamVR_TrackedObject trackedObj;
    bool startLook;
    // Use this for initialization
    void Start()
    {
        savePos = you.transform.rotation;
        startLook = true;
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if(!startLook)
            {
                savePos = you.transform.rotation;
                startLook = true;
                //Debug.Log("Setting start!");
            }
        }

    }

    void LateUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad) && startLook || Input.GetKey("b"))
        {
            //the rig I am using is totally screwed up and I don't have time to change it
            //so, these are the hoops we have to jump through in order to create the appropriate effect
            //that will mimic the gameObject "you" looking at the user.
            you.transform.LookAt(me.transform.position);
            you.transform.Rotate(transform.right * -1, 90);
            you.transform.Rotate(transform.up * -1, -90);
            
        }
    }
    
    void resetLook()
    {
        you.transform.rotation = savePos;
    }
}
