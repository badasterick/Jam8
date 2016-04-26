using UnityEngine;
using System.Collections;

public class ResetScript : MonoBehaviour {

	public GameObject lastCheckpoint;
	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.transform.position.y < 0.4) 
		{
			ResetPosition ();
		}
	}

	public void ResetPosition()
	{
		this.transform.position = lastCheckpoint.transform.position;
	}
}