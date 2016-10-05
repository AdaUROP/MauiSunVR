using UnityEngine;
using System.Collections;

public class ropeGrow : MonoBehaviour {

    public GameObject rope;
    public float timer, scale;
    float timerC;
    bool grow;
    Vector3 startScale, newScale;
	// Use this for initialization
	void Start () {

        startScale = gameObject.transform.localScale;
        newScale = new Vector3(scale, scale, scale);
    }
	
	// Update is called once per frame
	void Update () {

        if (grow)
        {
            scaleObj();
        }

    }

    void scaleObj()
    {
        if (timerC <= timer)
        {
            float complete = timerC / timer;
            rope.transform.localScale = Vector3.Lerp(startScale, newScale, complete);
            /*
             * foreach(Transform t in transform)
            {
                t.localScale = Vector3.Lerp(startScale, newScale, complete);
            }
            */
            timerC += Time.deltaTime;
        }
        if (timerC > timer)
        {
            timerC = 0;
            grow = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("areaLimit"))
        {
            grow = true;
        }
    }
}
