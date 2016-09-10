using UnityEngine;
using System.Collections;

public class changeBlendshape : MonoBehaviour {

    public float blendSpeed = 1f; //this is the speed of blending, also assignable from unity editor

	//wrapper class to blend shapes, takes the object to blend and the goal weight to blend to
	public void blendShape(GameObject blend, float goal){

        //in case vwe are given an object that does not blend, try/catch the call to retrieve the skinned mesh renderer on the object
        try {
            SkinnedMeshRenderer smr = blend.GetComponent<SkinnedMeshRenderer>();
            StartCoroutine(blendToGoal(smr, goal)); //once we have it, start the coroutine to blend
        }
        catch
        {   //if there is not a blendshape, print to the log
            Debug.Log("Object does not have a blendshape SkinnedMeshRenderer Component.");
        }
		
	}
	

    //coroutine to blend the shape to the weight goal
    IEnumerator blendToGoal(SkinnedMeshRenderer smr, float goal)
    {
        //retrieve the current weight and initialize the newWeight variable
        float currWeight = smr.GetBlendShapeWeight(0);
        float newWeight = 0;

        //check if this is a positive change in weight
        if(goal > currWeight && currWeight <= 101)
        {
            Debug.Log("Beginning blend loop!");

            //adjust the weight by the blendspeed until we hit the goal weight
            while (currWeight < goal)
            {
                newWeight = currWeight + blendSpeed;
                smr.SetBlendShapeWeight(0, newWeight);

                //update the current weight
                currWeight = newWeight;

                //pause for a small amount of time so the change is gradual
                yield return new WaitForSeconds(0.005f);
            }

        }
        //else this is a negative change in weight
        else if(goal < currWeight && currWeight >= 1){


            Debug.Log("Beginning blend loop!");

            //adjust the weight by the blendspeed until we hit the goal weight
            while (currWeight > goal)
            {
                newWeight = currWeight - blendSpeed;
                smr.SetBlendShapeWeight(0, newWeight);

                //update the current weight
                currWeight = newWeight;

                //pause for a small amount of time so the change is gradual
                yield return new WaitForSeconds(0.005f);
            }

        }

        
    }
}
