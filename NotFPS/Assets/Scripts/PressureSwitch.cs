using UnityEngine;
using System.Collections;

public class PressureSwitch : MonoBehaviour {
	public GameObject activatedObject;
	private ActivatableObject ao; 
	private bool pressed = false;
	// Use this for initialization
	void Start () {
		ao = activatedObject.GetComponent<ActivatableObject> ();
	}
	
	void OnTriggerEnter()
	{
		if (pressed == false) 
		{
			if (ao != null) {
				ao.ActivateObject ();
			}
			pressed = true;
		}
	}

	void OnTriggerExit()
	{
		if (pressed == true) 
		{
			if (ao != null) {
				ao.DeactivateObject ();
			}
			pressed = false;
		}
	}
}
