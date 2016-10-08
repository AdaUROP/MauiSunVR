using UnityEngine;
using System.Collections;

public class respawnHook : MonoBehaviour {
    public GameObject prefab;
    GameObject current;
    public bool respawn;
    respawnHook rH;
	// Use this for initialization
	void Start () {
        
        current = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (respawn)
        {
            Destroy(current);
            current = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
            respawn = false;
        }

	
	}
}
