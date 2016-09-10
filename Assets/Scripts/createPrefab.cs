using UnityEngine;
using System.Collections;

public class createPrefab : MonoBehaviour {

    public GameObject prefab;
    public SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }

    }
}
