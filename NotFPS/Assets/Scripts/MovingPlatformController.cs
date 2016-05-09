using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour, ActivatableObject {
	public bool isOn = true;
	public bool doesntDeactivate = false;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public float moveTime = 1.0f;
	private bool movingForward = true;
	private Vector3 moveSpeed;
	private Vector3 travelDistance;

	// Use this for initialization
	void Start () {
		travelDistance = endPosition - startPosition;

		moveSpeed = travelDistance / moveTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated) {
			
			transform.position += moveSpeed * Time.deltaTime;

			Vector3 dif = transform.position - startPosition;
			Vector3 eDif = transform.position - endPosition;
			if ((movingForward && Vector3.Dot (dif, travelDistance.normalized) >= travelDistance.magnitude) 
				|| (!movingForward && Vector3.Dot (eDif, -travelDistance.normalized) >= travelDistance.magnitude)) {
				moveSpeed *= -1;
				movingForward = !movingForward;
			}

		}
	}

	public bool isActivated {
		get {
			return isOn;
		}

		set {
			isOn = value;
		}
	}
	public void ActivateObject() {
		isActivated = true;
	}

	public void DeactivateObject() {
		if (!doesntDeactivate) {
			isActivated = false;
		}
	}
}
