using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false, dodge;
    bool dodging = false, forward = false, premature = false, emote = false;
    int interval = 20, d = 0, timesHit = 0;
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

    // Use this for initialization
    void Start () {
        group = GameObject.Find("sunModel");
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
                        originalRotation = this.transform.parent.rotation;
                        targetRotation = new Quaternion(originalRotation.x, .5f, originalRotation.z, originalRotation.w);
                        dodging = true;
                        forward = true;
                    }
                    else if (dodging && forward)
                    {
                        if (dodgeElapsed < dodgeTimer)
                        {
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(originalRotation, targetRotation, dodgeElapsed / dodgeTimer);
                        }
                        else
                        {
                            dodgeElapsed = 0;
                            forward = false;
                        }
                    }
                    else if (dodging && !forward)
                    {
                        if (dodgeElapsed < dodgeTimer)
                        {
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(targetRotation, originalRotation, dodgeElapsed / dodgeTimer);
                        }
                        else
                        {
                            dodgeElapsed = 0;
                            dodging = false;
                        }
                    }
                }
                else {
                    if (dodging && !premature)
                    {
                        forward = false;
                        premature = true;
                        dodgeTempTimer = dodgeTimer - dodgeElapsed;
                        dodgeElapsed = 0;
                        tempRotation = this.transform.parent.rotation;
                    }
                    else if (dodging && premature) {
                        if (dodgeElapsed < dodgeTempTimer)
                        {
                            dodgeElapsed += Time.deltaTime;
                            this.transform.parent.rotation = Quaternion.Slerp(tempRotation, originalRotation, dodgeElapsed / dodgeTempTimer);
                        }
                        else {
                            dodging = false;
                            premature = false;
                        }
                    }
                }
            }
        }

        if (collided)
        {
            GameObject.Find("Subtitles").SendMessage("displayScript", new SubtitleParams("OWWW!!! That hurts!", 90));
            changeEmotion(new SunEmotion(0f, 100f, 0f, 0f, 0f, 100f), 2.5f);
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

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            Debug.Log("That's a HIT");
            timesHit++;
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
            c.gameObject.transform.parent = gameObject.transform;
        }
    }

    public void sunHit(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            //Debug.Break();
            Debug.Log("That's a HIT");
            timesHit++;
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
            c.gameObject.transform.parent = gameObject.transform;
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
