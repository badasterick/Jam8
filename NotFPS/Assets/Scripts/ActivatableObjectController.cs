using UnityEngine;
using System.Collections;

public interface ActivatableObject {
	bool isActivated {
		get;
		set;
	}

	void ActivateObject ();
	void DeactivateObject();
}
