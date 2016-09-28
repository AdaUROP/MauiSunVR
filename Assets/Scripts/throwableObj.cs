using UnityEngine;
using System.Collections;

public class throwableObj : MonoBehaviour {

    public bool grow = false;
    public float scale, timer;
    public bool grabbed = false;

    Collider col;
    bool active = false;
    float timerC;
    Vector3 startScale;
    Vector3 newScale;
    // Use this for initialization
    void Start()
    {
        startScale = gameObject.transform.localScale;
        newScale = new Vector3(scale, scale, scale);
        col = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(grow)
        {
            scaleObj();
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
        }

        if(col.CompareTag("areaLimit") && active)
        {
            grow = true;
            timerC = 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(grow)
        {
            grow = false;
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

}
