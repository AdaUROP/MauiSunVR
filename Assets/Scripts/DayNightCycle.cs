using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    //the lights we need to adjust, sunLight will be the intensity of the light
    //skylight is the range of the point light to create glow effect
    public Light sunLight, skyLight;
    //these are all the transforms we will be manipulating, the transform of the actual sun, transform of the center world pivot and the transform for the actual directional light to emulate a day cycle
    public Transform sunXForm, centerXform, sunLXform;
    //the max/mins for sunlight and sky glow, also the amount of rotation to trigger each step.
    public float sunMax, sunMin, skyMax, skyMin, rotStep;
    private float rotation;
    private bool spinLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //key command for testing purposes, see public function below in order to trigger this same effect with a call
        if(Input.GetKey("space"))
        {
            //Debug.Log("hit");
            if(spinLight)
            {
                sunLXform.Rotate(rotStep, 0, 0);
            }

            sunXForm.Rotate(1, 0, 0);
            //sunXForm.RotateA(centerXform.right, rotStep);

            rotation += rotStep;

            setLights(rotation);
        }
	}

    //annoying range settings to determine what the light intensity should be, the appropriate rotation of the day light to emulate time changes and range of the sky glow, don't bother looking into this to deeply it's a mess
    void setLights(float rot)
    {
        if(rot >= 39)
        {
            spinLight = true;
        }
        if(rot >= 186)
        {
            spinLight = false;
        }

        if(rot > 0 && rot <= 90)
        {
            sunLight.intensity = Mathf.Lerp(sunMin, sunMax, rot / 90);
            skyLight.range = Mathf.Lerp(skyMin, skyMax, rot / 90);
        }
        if(rot > 90 && rot <= 135)
        {
            sunLight.intensity = sunMax;
            skyLight.range = skyMax;
        }
        if(rot > 135 && rot <= 225)
        {
            sunLight.intensity = Mathf.Lerp(sunMax, sunMin, (rot - 135) / 90);
            skyLight.range = Mathf.Lerp(skyMax, skyMin, (rot - 135) / 90);
        }
        if(rot > 225)
        {
            skyLight.range = skyMin;
            sunLight.intensity = sunMin;
        }
        if(rot >= 360)
        {
            rotation = 0;
            sunLXform.eulerAngles = new Vector3(14, 0, 0);
        }


    }

    //you can call this function from another script in order to get the same effect of holding or hitting the space button as in the update
    public void sunCycle()
    {
        if (spinLight)
        {
            sunLXform.Rotate(rotStep, 0, 0);
        }

        sunXForm.Rotate(1, 0, 0);

        rotation += rotStep;

        setLights(rotation);
    }
}
