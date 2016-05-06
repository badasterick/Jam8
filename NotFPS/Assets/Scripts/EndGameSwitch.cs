using UnityEngine;
using System.Collections;

public class EndGameSwitch : MonoBehaviour {
	public bool start = false;
	public bool end = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (start) {
			if (col.gameObject.tag == "Player") {
				Debug.Log ("Shakage");	
				col.gameObject.GetComponentInChildren<CameraShake> ().Shake ();
				col.gameObject.GetComponentInChildren<CameraShake> ().shake = true;
			}
		} else if (end) {
			if (col.gameObject.tag == "Player") {
				Debug.Log ("no bShakage");	
				//col.gameObject.GetComponentInChildren<CameraShake> ().Shake ();
				col.gameObject.GetComponentInChildren<CameraShake> ().shake = false;
			}
		}

	}
}
