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

	public virtual float PushForce {
		get {
			return Input.GetMouseButton(pushKey) ? 1.0f : 0.0f;
		}
	}

	public override bool UsesMouse {
		get {
			return true;
		}
	}
}
