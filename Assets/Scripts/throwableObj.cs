using UnityEngine;
using System.Collections;

public class throwableObj : MonoBehaviour {

    public bool grow = false;
    public float scale = 0;
    public float timer = 0;
    public bool grabbed = false;

    Collider colli              ;
    bool active = false;
    float timerC;
    Vector3 startScale;
    Vector3 newScale;
    Rigidbody rb;
    GameObject scriptB;
    // Use this for initialization
    void Start()
    {
        scriptB = GameObject.Find("ScriptBox");
        startScale = gameObject.transform.localScale;
        newScale = new Vector3(scale, scale, scale);
        colli = GetComponent<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(grow)
        {
            scaleObj();
        }
        if(grabbed && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
	
	}

    public void setReady()
    {
        active = true;
        gameObject.tag = "throwable";
    }
    public void disable()
    {
        active = false;
        gameObject.tag = "Untagged";
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("hand") && !active)
        {
            setReady();
            colli = null;
        }

        if(col.CompareTag("areaLimit") && active)
        {
            grow = true;
            timerC = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("hand"))
        {
            colli = col;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(grow)
        {
            grow = false;
        }
        if (col.gameObject.CompareTag("ground") || col.gameObject.CompareTag("pig") || col.gameObject.CompareTag("npc"))
        {
            if(Vector3.Magnitude(rb.velocity) > 2)
            {
                scriptB.SendMessage("emitCloudwSound", gameObject.transform);
            }
           
        }
    }

    void scaleObj()
    {
        if(timerC <= timer)
        {
            float complete = timerC / timer;
            gameObject.transform.localScale = Vector3.Lerp(startScale, newScale, complete);
            /*
             * foreach(Transform t in transform)
            {
                t.localScale = Vector3.Lerp(startScale, newScale, complete);
            }
            */
            timerC += Time.deltaTime;
        }
        if(timerC > timer)
        {
            timerC = 0;
            grow = false;
        }
        
    }

    public void stopGrow()
    {
        grow = false;
        Debug.Log("stop!");
    }

    public void setGrabbed(bool val)
    {
        grabbed = val;
    }

    public void dropMe()
    {
        if(colli != null)
        {
            colli.SendMessage("release");
        }
    }

}
