using UnityEngine;
using System.Collections;

public class moveOnInclines : MonoBehaviour {

    float initDistanceFromGround;
    RaycastHit hit;
    public Camera viveCam; // camera should be assigned from Unity editor

    // Use this for initialization
    void Start () {

        //find the difference in distance from the camera to the ground with and set initial distance of the world to the ground
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            print("Found object " + hit.collider.gameObject.tag + " - setting init distance: " + hit.distance);

            if (hit.collider.gameObject.tag == "ground") initDistanceFromGround = hit.distance; //if we don't hit "ground", there's probably something wrong...
            else {
                Debug.Log("No initial distance saved, no ground detected under the CameraRig object.");
            }
        }


    }

    void Update()
    {

        //check if camera moved (get camera y pos)
        Vector3 camPos = viveCam.gameObject.transform.position;

        //raycast from camera and see what we collide with
        if (Physics.Raycast(camPos, -Vector3.up, out hit))
        {
          //print("Found object " + hit.collider.gameObject.name +" - distance: " + hit.distance);
           // Debug.DrawLine(transform.position, hit.point);

            //set the distance of the camera from the ground to match our initial value
            if (hit.collider.gameObject.tag == "ground")
            {
                // print("hit pt y: " + hit.point.y + " intD: "+initDistanceFromGround+" new y: " + (hit.point.y + initDistanceFromGround));
                //set the room y location to match the position where the ray collided
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, hit.point.y + initDistanceFromGround, gameObject.transform.position.z);
            }
            else //again, if we didn't hit ground, nothing will change
            {
                //Debug.Log("No change to the CameraRig position as raycast did not detect ground.");
            }
        }
    }
}
