using UnityEngine;
using System.Collections;

public class piggyAI : MonoBehaviour {

    string piggyState = "idle";

    public GameObject player;
    public float separationLimit = 2;
    public float speed = 2;
    public string sceneTime = "night";
    public GameObject raycaster;

    private Vector3 moveGoal;
    private float distanceToGround = 0;
    private float idleTime;


    // Use this for initialization
    void Start() {

        idleTime = Time.time;

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
            }

        }
            Debug.Log(distanceToGround);

    }
	
	// Update is called once per frame
	void Update () {
        moveGoal = player.transform.position;

        checkState();
       // Debug.Log("Idle time: " + (Time.time - idleTime));
        Debug.Log("Piggy distance from player: " + Vector3.Distance(player.transform.position, transform.position));

	}

    void checkState()
    {
        //check the current state of piggy, since that will influence other states
        switch (piggyState)
        {
            case "idle": //do we want to only follow the player if we are idle? Or even if they are farther away even if sleeping?
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance >= separationLimit)
                {
                    moveGoal = player.transform.position;
                    move();
                }
                else if((Time.time - idleTime) > 20)
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
            default:
                returnToIdle();
                break;


        }
        
    }



    public void move()
    {
        Debug.Log("Piggy is moving!");
        piggyState = "moving";
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
        idleTime = Time.time;
    }

    IEnumerator moveTo()
    { 

        while (transform.position != moveGoal && piggyState == "moving")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                Debug.DrawLine(transform.position, hit.point);
                Debug.Log("Found object " + hit.collider.gameObject.tag + " - distance: " + hit.distance);
                if (hit.collider.gameObject.tag == "ground")
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                }
                else
                {
                    Debug.Log("piggy did not hit ground!!");
                }
            }

            //rotate piggy
            var rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 15);

            transform.position = Vector3.MoveTowards(transform.position, moveGoal, speed * Time.deltaTime);
           


                yield return new WaitForSeconds(1f);
        }
        if(piggyState == "move") returnToIdle();
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
