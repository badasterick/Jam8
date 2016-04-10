using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
	public GameObject gate;
	private bool pressed;
	// Use this for initialization
	void Start () 
	{
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter()
	{
		if (pressed == false) 
		{
			gate.transform.position = gate.transform.position + new Vector3 (0, 5, 0);
			pressed = true;
		}
	}

	void OnTriggerExit()
	{
		if (pressed == true) 
		{
			gate.transform.position = gate.transform.position + new Vector3 (0, -5, 0);
			pressed = false;
		}
	}
}
