using UnityEngine;
using System.Collections;

public class triggerScript : MonoBehaviour {

    //this trigger script just adjusts the expression when you hold down keyboard keys. 
    //We will be writing other trigger scripts for making blendshape changes later based on game state or in game triggers fired

    public GameObject blendObject; //this will be the the object with the changeBlendshape script attached

    //individual blendshape goals
    float teethGoal = 0;
    float faceGoal = 0;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("openTeeth")) //input set to when "A" key is held down
        {
            Debug.Log("opening teeth");
            teethGoal++; //increase the goal by 1
            //call the blendshape function on the blendObject to the shape we want to change to the new goal amount
            blendObject.GetComponent<changeBlendshape>().blendShape(GameObject.Find("teethSmile"), teethGoal); 
        }
        if (Input.GetButton("closeTeeth")){ //input set to when "D" key is held down
            Debug.Log("close teeth");
            teethGoal--; //decrease the goal by 1
            //call the blendshape function on the blendObject to the shape we want to change to the new goal amount
            blendObject.GetComponent<changeBlendshape>().blendShape(GameObject.Find("teethSmile"), teethGoal);
        }
        if (Input.GetButton("happyFace")){ //input set to when "W" key is held down
            Debug.Log("happy face");
            faceGoal--; //decrease the goal by 1
            //call the blendshape function on the blendObject to the shape we want to change to the new goal amount
            blendObject.GetComponent<changeBlendshape>().blendShape(GameObject.Find("Group3"), faceGoal);
        }
        if (Input.GetButton("sadFace")){ //input set to when "S" key is held down
            Debug.Log("sad face");
            faceGoal++; //increase the goal
            //call the blendshape function on the blendObject to the shape we want to change to the new goal amount
            blendObject.GetComponent<changeBlendshape>().blendShape(GameObject.Find("Group3"), faceGoal);
        }
	
	}
}
