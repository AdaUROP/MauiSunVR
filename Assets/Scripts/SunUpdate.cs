using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false;
    int interval = 20, d = 0, timesHit = 0;
    Color originalColor;
    public Color targetColor;
    public float timer;
    float elapsed = 0;
    GameObject group;
    SkinnedMeshRenderer sunSmile;
    public float targetWeight = 50;

    // Use this for initialization
    void Start () {
        group = GameObject.Find("Group3");
        sunSmile = group.GetComponent<SkinnedMeshRenderer>();
        originalColor = group.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (collided)
        {
            GameObject.Find("Subtitles").SendMessage("displayScript", new SubtitleParams("OWWW!!! That hurts!", 90));
            sunSmile.SetBlendShapeWeight(0, sunSmile.GetBlendShapeWeight(0) + 2f);
            if (elapsed <= timer)
            {
                elapsed += Time.deltaTime;
                sunSmile.SetBlendShapeWeight(0, targetWeight * elapsed / timer);
                group.GetComponent<Renderer>().material.color = Color.Lerp(originalColor, targetColor, elapsed / timer >= 1 ? 1 : elapsed / timer);
            }
            else
            {
                originalColor = targetColor;
                elapsed = 0;
                collided = false;
            }
            d++;
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "throwable")
        {
            timesHit++;
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
            c.gameObject.transform.parent = gameObject.transform;
        }
    }


}
