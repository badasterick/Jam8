using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
	public GameObject gate;
	private bool pressed;
    AudioSource a;
	// Use this for initialization
	void Start () 
	{
		pressed = false;
        a = GetComponent<AudioSource>();
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
            a.Play();
		}
	}

	void OnTriggerExit()
	{
		if (pressed == true) 
		{
			gate.transform.position = gate.transform.position + new Vector3 (0, -5, 0);
			pressed = false;
            a.Play();
		}
	}
}
