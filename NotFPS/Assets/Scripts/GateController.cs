using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour, ActivatableObject {

	private DoorState curState;
	public bool startOpen = false;
	public bool isOpen = false;
	private bool isOpening = false;
	private float height;
	public float openTime = 1.0f;
	private float startY;
	private float openSpeed = 1;
	// Use this for initialization
	void Start () {
		height = GetComponent<Renderer> ().bounds.size.y;
		startY = transform.position.y;
		openSpeed = height / openTime;

		if (startOpen) {
			isOpen = true;
			transform.position += new Vector3 (0, height, 0);
			curState = DoorState.Open;
		} else {
			curState = DoorState.Closed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isActivated {
		get {
			return isOpening;
		}

		set {
			isOpening = value;
		}
	}

	public void ActivateObject() {
		if (curState == DoorState.Closing || curState == DoorState.Closed) {
			if (curState == DoorState.Closing) {
				StopCoroutine (CloseGate());
			}
			curState = DoorState.Opening;
			StartCoroutine (OpenGate ());
		}
	}

	public void DeactivateObject() {
		if (curState == DoorState.Opening || curState == DoorState.Open) {
			if (curState == DoorState.Opening) {
				Debug.Log ("Closing");
				StopCoroutine (OpenGate ());
			}
			curState = DoorState.Closing;
			StartCoroutine (CloseGate ());
		}
	}

	IEnumerator OpenGate() {
		while (transform.position.y < startY + height && curState == DoorState.Opening) {
			transform.position += new Vector3 (0, 1, 0) * openSpeed * Time.deltaTime;
			yield return null;
		}
		if (curState == DoorState.Opening) {
			curState = DoorState.Open;
		}
		yield return null;
	}

	IEnumerator CloseGate() {
		
		while (transform.position.y > startY && curState == DoorState.Closing) {
			transform.position -= new Vector3 (0, 1, 0) * openSpeed * Time.deltaTime;
			yield return null;
		}

		if (curState == DoorState.Closing) {
			curState = DoorState.Closed;
		}

		yield return null;
	}
}

public enum DoorState {
	Open, Opening, Closing, Closed
}
		
