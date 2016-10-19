using UnityEngine;
using System.Collections;

public class throwScript : MonoBehaviour {

    public GameObject grabObj;
    public Rigidbody attachPoint;
    public float forceMult;

    public SteamVR_TrackedObject trackedObj;
    FixedJoint joint;
    bool holding = false;

    // Use this for initialization
    void Start () {
        //trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        //if(grabObj != null && device.GetPress(SteamVR_Controller.ButtonMask.Grip) && holding)
        //{
        //    grabObj.SendMessage("setKin", true);
        //}


        if(grabObj != null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !holding)
        {
            
            if(grabObj.name == "hook")
            {
                grabObj.SendMessage("setKin", false);
            }
            grabObj.transform.position = attachPoint.transform.position;
            //Debug.Log("adding joint1");
            joint = grabObj.AddComponent<FixedJoint>();
            joint.connectedBody = attachPoint;

            grabObj.SendMessage("disable");
            grabObj.SendMessage("setGrabbed", true);
            holding = true;
            
            
        }
        else if (grabObj != null && holding && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            var go = joint.gameObject;
            var rb = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            //Object.Destroy(go, 15.0f);

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                if(grabObj.name == "hook")
                {
                    rb.velocity = origin.TransformVector(device.velocity * forceMult);
                    rb.angularVelocity = origin.TransformVector(device.angularVelocity * forceMult);
                }
                else
                {
                    rb.velocity = origin.TransformVector(device.velocity);
                    rb.angularVelocity = origin.TransformVector(device.angularVelocity);
                }

                if(Vector3.Magnitude(rb.velocity) > 4)
                {
                    rb.BroadcastMessage("wooshFX");
                }

            }
            else
            {
                if (grabObj.name == "hook")
                {
                    rb.velocity = origin.TransformVector(device.velocity * forceMult);
                    rb.angularVelocity = origin.TransformVector(device.angularVelocity * forceMult);
                }
                else
                {
                    rb.velocity = origin.TransformVector(device.velocity);
                    rb.angularVelocity = origin.TransformVector(device.angularVelocity);
                }

                if (Vector3.Magnitude(rb.velocity) > 4)
                {
                    rb.BroadcastMessage("wooshFX");
                }
            }
            go.BroadcastMessage("startArc");
            rb.maxAngularVelocity = rb.angularVelocity.magnitude;
            grabObj.SendMessage("setReady");
            grabObj.SendMessage("setGrabbed", false);
            holding = false;
            grabObj = null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (grabObj == null)
        {
            //Debug.Log("Trig Hit");
            if (col.CompareTag("throwable"))
            {
               // Debug.Log("Hit throwable");
                grabObj = col.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(grabObj != null)
        { 
        if(col.CompareTag("throwable") && !holding)
        {
            //Debug.Log("Exit throwable");
            grabObj = null;
        }
        }
    }

    public void release()
    {
        var go = joint.gameObject;
        var rb = go.GetComponent<Rigidbody>();
        Object.DestroyImmediate(joint);
        joint = null;
        grabObj.SendMessage("setReady");
        grabObj.SendMessage("setGrabbed", false);
        holding = false;
        grabObj = null;
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Hit");
        if (col.gameObject.CompareTag("throwable"))
        {
            Debug.Log("Hit throwable");
            grabObj = col.gameObject;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("throwable"))
        {
            Debug.Log("Exit collision");
            grabObj = null;
        }
    }
    */
}
