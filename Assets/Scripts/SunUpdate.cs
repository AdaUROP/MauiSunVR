using UnityEngine;
using System.Collections;

public class SunUpdate : MonoBehaviour {

	public bool collided = false, dodge;
    int interval = 20, d = 0, timesHit = 0;
    Color originalColor;
    public Color targetColor;
    public float timer;
    float elapsed = 0;
    GameObject group;
    SkinnedMeshRenderer sunSmile;
    public float targetWeight = 50;
    RaycastHit hit;
    Quaternion originalRotation, targetRotation;

    // Use this for initialization
    void Start () {
        group = GameObject.Find("sunModel");
        sunSmile = group.GetComponent<SkinnedMeshRenderer>();
        originalColor = group.GetComponent<Renderer>().material.color;
        // originalRotation = this.transform.parent.rotation;
        // targetRotation = new Quaternion(originalRotation.x, .3f, originalRotation.z, originalRotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        if (dodge)
        {
            if (Physics.SphereCast(group.transform.position, 10, group.transform.forward, out hit, 300))
            {
                print("Meh");
                if (hit.collider.CompareTag("throwable"))
                {
                    originalRotation = this.transform.parent.rotation;
                    targetRotation = new Quaternion(originalRotation.x, .3f, originalRotation.z, originalRotation.w);
                    this.transform.parent.rotation = Quaternion.Slerp(originalRotation, targetRotation, hit.distance < 100 ? (100 - hit.distance / 100) : 0);
                }
            }
        }

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
            Debug.Log("That's a HIT");
            timesHit++;
            collided = true;
            Rigidbody rb = c.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.SendMessage("stopGrow");
            c.gameObject.transform.parent = gameObject.transform;
        }
    }


}
