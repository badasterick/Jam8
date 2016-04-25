using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class KeyboardController: GenericController{
	protected string leftKey = "";
	protected string rightKey = "";
	protected string upKey = "";
	protected string downKey = "";
	protected new string enterKey = "";
	protected new string backKey = "";
	protected new string jumpKey;
	protected new string interactKey;
	protected new string swapKey;
	private bool useMouse = false;
	private float lookRectWidth = 100;
	private float lookRectHeight = 100;
	private Rect lookRect;
	private Vector3 screenCenter;
	public override void LoadController() {
		JSONNode jsn = null;
		StreamReader str = new StreamReader ("Assets/Json/keyboard.json");

		if (str != null) {
			jsn = JSON.Parse (str.ReadToEnd ());
			leftKey = jsn ["left"];
			rightKey = jsn ["right"];
			upKey = jsn ["up"];
			downKey = jsn ["down"];
			useMouse = jsn ["mouse"].AsBool;
			backKey = jsn ["back"];
			enterKey = jsn ["enter"];

			jumpKey = jsn ["jump"];
			interactKey = jsn ["interact"];
			swapKey = jsn ["swap"];

			pushKey = jsn ["push"].AsInt;

			pullKey = jsn ["pull"].AsInt;

			Vector3 lookRectSize = new Vector3 (lookRectWidth, lookRectHeight, 0);
			screenCenter = new Vector3 (Screen.width, Screen.height, 0) / 2;
			lookRect = new Rect (screenCenter - (lookRectSize / 2), lookRectSize);
		}
	}

	public override float Horizontal { 
		get { 
			return (GetKey (rightKey) ? 1 : 0) - (GetKey (leftKey) ? 1 : 0); 
		}
	}

	public override float Vertical {
		get {
			return (GetKey (upKey) ? 1 : 0) - (GetKey (downKey) ? 1 : 0);
		}
	}

	public override int HorizontalDown { 
		get { 
			return (GetKeyDown (rightKey) ? 1 : 0) - (GetKeyDown (leftKey) ? 1 : 0); 
		}
	}

	public override int VerticalDown {
		get {
			return (GetKeyDown (upKey) ? 1 : 0) - (GetKeyDown (downKey) ? 1 : 0);
		}
	}

	public override bool IsEnterDown {
		get {
			if (useMouse) {
				return Input.GetMouseButtonDown (0) || GetKeyDown(enterKey);
			} else {
				return GetKeyDown (enterKey);
			}
		}
	}
	public override bool IsBackDown {
		get {
			return GetKeyDown (backKey);
		}
	}

	public override bool IsSwapDown {
		get {
			return GetKeyDown (swapKey);
		}
	}

	public override bool IsInteractDown {
		get {
			return GetKeyDown (interactKey);
		}
	}

	public override bool IsJumpDown {
		get {
			return GetKeyDown (jumpKey);
		}
	}
		
	public override bool GetKey(string button) {
		KeyCode kc = (KeyCode)System.Enum.Parse (typeof(KeyCode), button);

		return Input.GetKey(kc);
	}
	public override bool GetKeyDown(string button) {
		KeyCode kc = (KeyCode)System.Enum.Parse (typeof(KeyCode), button);

		return Input.GetKeyDown(kc);
	}

	public override float PullForce {
		get {
			return Input.GetMouseButton(pullKey) ? 1.0f : 0.0f;
		}
	}

	public override float PushForce {
		get {
			return Input.GetMouseButton(pushKey) ? 1.0f : 0.0f;
		}
	}

	public override bool UsesMouse {
		get {
			return true;
		}
	}

	public override void RotateCamera ()
	{
		Camera cam = Camera.main;

		float horz = 0;
		float vert = 0;

	
		Vector3 dif = Input.mousePosition - screenCenter;

		horz = Input.GetAxis ("Mouse X");
		vert = -Input.GetAxis ("Mouse Y");


		hAngle += horz * maxRotSpeed * Time.deltaTime;
		vAngle = Mathf.Clamp (vAngle + (vert * maxRotSpeed * Time.deltaTime), -maxYLook, maxYLook);

		Quaternion spinQuat = Quaternion.AngleAxis (hAngle, Vector3.up);
		Quaternion lookQuat = Quaternion.AngleAxis (vAngle, Vector3.right);
		cam.transform.rotation = spinQuat * lookQuat;
	}
}
