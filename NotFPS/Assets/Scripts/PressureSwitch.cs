using UnityEngine;
using System.Collections;

public class PressureSwitch : MonoBehaviour {
	public GameObject activatedObject;
	private ActivatableObject ao; 
	private bool pressed = false;
	private int numberOfItemsOnTop = 0;
	// Use this for initialization
	void Start () {
		ao = activatedObject.GetComponent<ActivatableObject> ();
	}

	void OnTriggerEnter()
	{
		NumberOfItemsOnTop++;
	}

	void OnTriggerExit()
	{
		NumberOfItemsOnTop--;
	}

	int NumberOfItemsOnTop {
		set {
			if (NumberOfItemsOnTop == 0 && value > 0) {
				if (ao != null) {
					ao.ActivateObject ();
				}
			} else if (value == 0 && numberOfItemsOnTop > 0) {
				if (ao != null) {
					ao.DeactivateObject ();
				}
			}
			numberOfItemsOnTop = value;
		}

		get {
			return numberOfItemsOnTop;
		}
	}

	bool Pressed {
		get {
			return numberOfItemsOnTop > 0;
		}
	}
}
