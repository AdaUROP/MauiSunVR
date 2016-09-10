using UnityEngine;
using System.Collections;

public class proximityEvent : MonoBehaviour
{

    private bool allowProximityEvent = true;
    private Animator anim;
    public GameObject viveCam, focusTarget;
    private Vector3 viveCamPos;
    

    public float lookAtAngleRange;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {


        viveCamPos = viveCam.transform.position;
        //calculate the angle distance like we are doing for the color stuff
        Vector3 targetDir = viveCamPos - focusTarget.transform.position;
        //get the angle between the two vectors of our looking direction and the target look direction
        float angle = Vector3.Angle(targetDir, -viveCam.transform.forward);
        //Debug.Log(angle);
        if (angle < lookAtAngleRange)
        {
            //if player is looking within the angle range, then we can play the other animation, so set the other boolean
            anim.SetBool("lookingAt", true);
        }
        else
        {
            //else reverse the animation and set the boolean to false -> it goes back to the approaching animation
            anim.SetBool("lookingAt", false);
        }

    }

    public void toggleProximityEvent(bool newVal)
    {
        allowProximityEvent = newVal;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered!");
        if (allowProximityEvent == true && other.tag == "Player")
        {
            anim.SetBool("approached", true);
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exited!");
        if(other.tag == "Player")
        {
            anim.SetBool("approached", false);
        }
    }


}
