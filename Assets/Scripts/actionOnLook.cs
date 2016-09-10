using UnityEngine;
using System.Collections;

public class actionOnLook : MonoBehaviour
{

    public int speed = 1;
    private Animation anim;
    public string animName;
    private float curr_time;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (anim.isPlaying)
        {
            curr_time = anim[animName].time;
        }
    }

    //make another function that would be called if looked at
    public void action(bool isLooking)
    {
        //Debug.Log("Sphere was looked at!");
        if (isLooking == true && anim.isPlaying == false)
        {
            anim[animName].speed = 1;
            anim.Play();
        }

        else if (isLooking == false)
        {
            anim[animName].speed = -1;
            anim[animName].time = curr_time;
            anim.Play();

        }

    }

 
}
