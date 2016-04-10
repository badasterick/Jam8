using UnityEngine;
using System.Collections;

public class YBResetScript : MonoBehaviour {
	private Vector3 startpos;
	// Use this for initialization
	void Start () {
		startpos = this.transform.position;

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Reset")) {
			Debug.Log ("R key down");
			Reset ();
		}

	}

	void Reset()
	{
		this.transform.position = startpos;
	}
}
