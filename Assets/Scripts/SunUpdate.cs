﻿using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false, dodge;
    bool dodging = false, forward = false, premature = false, emote = false;
    int interval = 20, d = 0, timesHit = 0, timesDodged = 0;
    Color originalColor;
    public Color targetColor;
    public float timer, dodgeTimer, faceTimer;
    float dodgeTempTimer;
    float elapsed = 0, dodgeElapsed = 0, faceElapsed = 0;
    GameObject group;
    SkinnedMeshRenderer sunSmile;
    public float targetWeight = 50;
    RaycastHit hit;
    Quaternion originalRotation, targetRotation, tempRotation;
    SunEmotion currentEmotion, targetEmotion;
    GameObject scriptBox;
    public Rigidbody attach;

    // Use this for initialization
    void Start () {
        group = GameObject.Find("sunModel");
        scriptBox = GameObject.Find("ScriptBox");
        sunSmile = group.GetComponent<SkinnedMeshRenderer>();
        originalColor = group.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.R))
        {
            changeEmotion(new SunEmotion(0f, 0f, 0f, 0f, 0f, 0f), .2f);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            changeEmotion(new SunEmotion(75f, 100f, 0f, 50f, 0f, 0f), .1f);
        }
        if (dodge)
        {
            if (Physics.SphereCast(group.transform.position, 10, group.transform.forward, out hit, 300))
            {
                print("Meh");
                if (hit.collider.CompareTag("throwable"))
                {

                    if (!dodging && !forward)
                    {
                        print("Ponies");
                        originalRotation = this.transform.parent.rotation;
                        targetRotation = new Quaternion(originalRotation.x, .72f, originalRotation.z, originalRotation.w);
                        dodging = true;
                        forward = true;
                    }
                    else if (dodging && forward)
                    {
                        print("are");
                        if (dodgeElapsed < dodgeTimer * (Mathf.Min(timesDodged, 3) / 3 + 1))
                        {
                            print("not");
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(originalRotation, targetRotation, dodgeElapsed / (dodgeTimer * (Mathf.Min(timesDodged, 3) / 3 + 1)));
                        }
                        else
                        {
                            print("people.");
                            dodgeElapsed = 0;
                            forward = false;
                        }
                    }
                    else if (dodging && !forward && !premature)
                    {
                        print("Deal with it");
                        if (dodgeElapsed < dodgeTimer * (Mathf.Min(timesDodged, 3) / 3 + 1))
                        {
                            print("!");
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(targetRotation, originalRotation, dodgeElapsed / (dodgeTimer * (Mathf.Min(timesDodged, 3) / 3 + 1)));
                        }
                        else
                        {
                            print("NO!");
                            dodgeElapsed = 0;
                            dodging = false;
                            timesDodged++;
                        }
                    }
                }
                else {
                    if (dodging && !premature)
                    {
                        print("Oopsies...");
                        forward = false;
                        premature = true;
                        dodgeTempTimer = dodgeTimer - dodgeElapsed;
                        dodgeElapsed = 0;
                        tempRotation = this.transform.parent.rotation;
                    }
                    else if (dodging && premature) {
                        print("Meh.");
                        if (dodgeElapsed < dodgeTempTimer)
                        {
                            print("Clean up!");
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(tempRotation, originalRotation, dodgeElapsed / dodgeTempTimer);
                        }
                        else {
                            print("I mean it!!!");
                            dodging = false;
                            premature = false;
                            timesDodged++;
                        }
                    }
                }
            }
        }

        if (collided)
        {
            GameObject.Find("Subtitles").SendMessage("displayScript", new SubtitleParams("OWWW!!! That hurts!", 90));
            changeEmotion(new SunEmotion(0f, 100f, 0f, 0f, 0f, 100f), .5f);
            if (elapsed <= timer)
            {
                elapsed += Time.deltaTime;
                group.GetComponent<Renderer>().material.color = Color.Lerp(originalColor, targetColor, elapsed / timer);
                dodge = false;
            }
            else
            {
                originalColor = targetColor;
                elapsed = 0;
                collided = false;
            }
            d++;
        }

        if (emote)
        {
            if (faceElapsed <= faceTimer)
            {
                faceElapsed += Time.deltaTime;
                sunSmile.SetBlendShapeWeight(0, Mathf.Lerp(currentEmotion.getSmiling(), targetEmotion.getSmiling(), faceElapsed / faceTimer));
                sunSmile.SetBlendShapeWeight(1, Mathf.Lerp(currentEmotion.getAngryEyes(), targetEmotion.getAngryEyes(), faceElapsed / faceTimer));
                sunSmile.SetBlendShapeWeight(2, Mathf.Lerp(currentEmotion.getSad(), targetEmotion.getSad(), faceElapsed / faceTimer));
                sunSmile.SetBlendShapeWeight(3, Mathf.Lerp(currentEmotion.getClosed(), targetEmotion.getClosed(), faceElapsed / faceTimer));
                sunSmile.SetBlendShapeWeight(4, Mathf.Lerp(currentEmotion.getPursed(), targetEmotion.getPursed(), faceElapsed / faceTimer));
                sunSmile.SetBlendShapeWeight(5, Mathf.Lerp(currentEmotion.getAngry(), targetEmotion.getAngry(), faceElapsed / faceTimer));
            }
            else {
                emote = false;
                faceElapsed = 0;
            }
        }
    }

    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.gameObject.tag == "throwable")
    //    {
    //        scriptBox.BroadcastMessage("setSpring");
    //        timesHit++;
    //        collided = true;
    //        Rigidbody rb = c.GetComponent<Rigidbody>();
    //        rb.isKinematic = true;
    //        rb.SendMessage("stopGrow");
    //        ConfigurableJoint cj = rb.gameObject.AddComponent<ConfigurableJoint>();

    //        cj.connectedBody = attach;

    //        cj.enablePreprocessing = false;
    //        cj.projectionMode = JointProjectionMode.PositionAndRotation;
    //        cj.yMotion = ConfigurableJointMotion.Locked;
    //        cj.xMotion = ConfigurableJointMotion.Locked;
    //        cj.zMotion = ConfigurableJointMotion.Locked;
    //        rb.isKinematic = false;
    //        rb.useGravity = false;
    //        //gameObject.transform.parent = rb.gameObject.transform;
    //        //c.gameObject.transform.parent = gameObject.transform;
    //    }
    //}

    public void sunHit(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            // Debug.Log("That's a HIT");


            timesHit++;
            collided = true;

            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            ConfigurableJoint cj = rb.gameObject.AddComponent<ConfigurableJoint>();
            cj.connectedBody = attach;
            cj.enablePreprocessing = false;
            cj.projectionMode = JointProjectionMode.PositionAndRotation;
            cj.yMotion = cj.zMotion = cj.xMotion = cj.angularYMotion = cj.angularXMotion = cj.angularZMotion = ConfigurableJointMotion.Locked;
                        
            rb.isKinematic = false;
            rb.useGravity = false;


            scriptBox.BroadcastMessage("setSpring");
            scriptBox.BroadcastMessage("fightSong");
            group.BroadcastMessage("setLaugh", false);
            //gameObject.transform.parent = rb.gameObject.transform;
            //c.gameObject.transform.parent = gameObject.transform;

        }
    }

    public void changeEmotion(SunEmotion e, float t) {
        targetEmotion = e;
        currentEmotion = new SunEmotion(sunSmile.GetBlendShapeWeight(0), sunSmile.GetBlendShapeWeight(1), sunSmile.GetBlendShapeWeight(2), sunSmile.GetBlendShapeWeight(3), sunSmile.GetBlendShapeWeight(4), sunSmile.GetBlendShapeWeight(5));
        faceTimer = t;
        faceElapsed = 0;
        emote = true;
    }
}
