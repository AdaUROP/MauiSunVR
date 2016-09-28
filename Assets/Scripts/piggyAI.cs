using UnityEngine;
using System.Collections;

public class piggyAI : MonoBehaviour {

    string piggyState = "idle";

    public GameObject player;
    public float separationLimit = 2;
    public float speed = 2;
    public string sceneTime = "night";
    public GameObject raycaster;

    private Animator anim;
    private Vector3 moveGoal;
    private float distanceToGround = 0;
    private float idleTime;
    private float timer, timerCheck;


    // Use this for initialization
    void Start() {

        idleTime = Time.time;
        anim = gameObject.GetComponent<Animator>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.gameObject.tag == "ground")
            {
                distanceToGround = hit.distance;
                
            }
            else
            {
                Debug.Log("Ground was not found, piggy has no ground reference");
                Debug.Log("Piggy found " + hit.collider.gameObject.name);
            }

        }
            //Debug.Log(distanceToGround);

    }
	
	// Update is called once per frame
	void Update () {
        moveGoal = player.transform.position;

        checkState();
       // Debug.Log("Idle time: " + (Time.time - idleTime));
        //Debug.Log("Piggy distance from player: " + Vector3.Distance(player.transform.position, transform.position));

	}

    void checkState()
    {
        //check the current state of piggy, since that will influence other states
        switch (piggyState)
        {
            case "idle": //do we want to only follow the player if we are idle? Or even if they are farther away even if sleeping?

                Vector3 playerDist = new Vector3(player.transform.position.x, raycaster.transform.position.y, player.transform.position.z);

                float distance = Vector3.Distance(playerDist, raycaster.transform.position);
                if (distance >= separationLimit)
                {
                    move();
                }
                else if((Time.time - idleTime) > 40)
                {
                    
                    if(sceneTime == "day")
                    {
                        piggyState = "hot";
                        getHot();
                    }
                    else if(sceneTime == "night")
                    {
                        piggyState = "sleeping";
                        sleep();
                    }
                    
                }
                break;
            case "sleeping":
                //check if day, wake
                // else no change
                break;
            case "hot":
                //check if night, idle
                //else no change
                break;
            case "eating":
                //im thinking eating will be it's own coroutine, so we may not need this
                break;
            case "fetching":
                //again, might be it's own thing, idk yet
                break;
            case "scared":
                //maybe check to see if still need to be scared?
                //else return to idle
                break;
            case "petted":
                //as with scared, check if we still are being petted?
                //else return to idle
                break;
            case "moving":
                //moving or running
                break;
            default:
                returnToIdle();
                break;


        }
        
    }



    public void move()
    {
        //Debug.Log("Piggy is moving!");
        piggyState = "moving";
        anim.SetBool("idle", false);
        StartCoroutine("moveTo");
    }

    public void fetch()
    {
        Debug.Log("Piggy is fetching!");
        // if player picks up fetchable item
        // if item is released
        // move towards item at speed y
        // at distance z from object, slow speed to 0 until we reach item
        // pick up object (make item child of pig)
        // move towards player at speed y
        // at distance x from player, slow speed to 0
        // drop object (make item no longer pig child object)

    }

    public void sleep()
    {
        Debug.Log("Piggy is sleeping!");
        //play sleep animation
        //ignore other triggers until "awakened"

    }

    public void getHot()
    {
        Debug.Log("Piggy is hot!");
            // play hot animation

    }

    public void getScared()
    {
        Debug.Log("Piggy is scared!");
        // if we are in "scared" state->run scared animation

    }

    public void petted()
    {
        Debug.Log("Piggy is being petted!");
        // if player is touching pig->run animation
    }

    public void eat()
    {
        Debug.Log("Piggy is eating!");
        //if food object is placed by mouth (this might have to be a trigger set up) -> run eat animation
        // Destroy food object after running animation
    }

    public void returnToIdle()
    {
        Debug.Log("Piggy is idle!");
        piggyState = "idle";
        anim.SetBool("idle", true);
        idleTime = Time.time;
    }

    IEnumerator moveTo()
    {
        moveGoal = player.transform.position;
        Vector3 origPos = gameObject.transform.position;
        float range = Vector3.Distance(player.transform.position, transform.position);
        timer = range / speed;
        timerCheck = 0;


        while (transform.position != moveGoal && piggyState == "moving")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                Debug.DrawLine(transform.position, hit.point);
                //Debug.Log("Found object " + hit.collider.gameObject.tag + " - distance: " + hit.distance);
                if (hit.collider.gameObject.tag == "ground")
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                }
                else
                {
                    //Debug.Log("piggy did not hit ground!!");
                }
            }

            //rotate piggy
            Vector3 displacement = player.transform.position - transform.position;
            displacement.y = 0;

            var rotation = Quaternion.LookRotation(displacement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

            timerCheck += Time.deltaTime;
            float comp = timerCheck / timer;
            if(comp > 1 || (Vector3.Distance(player.transform.position, transform.position) < separationLimit/2))
            {
                break;
            }

            displacement = Vector3.Lerp(origPos, moveGoal, comp);
            displacement.y = origPos.y;
            Vector3 check = player.transform.position;
            check.y = origPos.y;

            if (comp > 1 || Vector3.Distance(check, displacement ) < separationLimit / 2)
            {
                break;
            }

            transform.position = displacement;
            //transform.position = Vector3.MoveTowards(transform.position, moveGoal, speed * Time.deltaTime);

            yield return new WaitForSeconds(0f);
            //yield return new WaitForSeconds(0.5f);

        }
        if(piggyState == "moving") returnToIdle();
        
    }

    void onColliderEnter(Collision other)
    {
        //we may need some transition here between states, particularily if the pig is hot or sleeping

        if(other.gameObject.tag == "Player")
        {
            piggyState = "petted";
            petted();
        }
        else if(other.gameObject.tag != "ground")
        {
            piggyState = "scared";
            //do we want the pig to move away first
                // if hit by object -> move away (would direction matter ?) at speed y
                // once x distance away, run animation

            getScared();
        }

    }
}
