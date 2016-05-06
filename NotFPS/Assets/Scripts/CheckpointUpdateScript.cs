using UnityEngine;
using System.Collections;

public class CheckpointUpdateScript : MonoBehaviour {

	public GameObject fpsController;
	public Vector3 forward = Vector3.forward;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
			fpsController.GetComponent<ResetScript> ().lastCheckpoint = gameObject;
            GetComponent<AudioSource>().Play();
        }
    }
}
