using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class GamepadController: GenericController {
	public int aButton = 0;
	public int bButton = 0;
	public int xButton = 0;
	public int yButton = 0;
	public string lxAxis = "";
	public string lyAxis = "";
	public string rxAxis = "";
	public string ryAxis = "";
	public string leftTrigger = "";
	public string rightTrigger = "";
	public float hAngle = 0;
	public float vAngle = 0;

	public override void LoadController() {
		JSONNode jsn = null;
		string os = SystemInfo.operatingSystem;
		StreamReader str = null;
		if (os.Contains ("Mac")) {
			str = new StreamReader ("Assets/Json/mac.json");
		} else if (os.Contains ("Windows")) {
			str = new StreamReader ("Assets/Json/windows.json");
			Debug.Log("Windows");
		} else if (os.Contains ("Linux")) {
			str = new StreamReader ("Assets/Json/linux.json");
		}

		if (str != null) {
			jsn = JSON.Parse (str.ReadToEnd ());
			jumpKey = jsn ["jump"].AsInt;
		
			enterKey = jsn ["enter"].AsInt;
			backKey = jsn ["back"].AsInt;
			swapKey = jsn ["swap"].AsInt;
			interactKey = jsn ["interact"].AsInt;

			lxAxis = jsn ["left-x"];
			lyAxis = jsn ["left-y"];
			rxAxis = jsn ["right-x"];
			ryAxis = jsn ["right-y"];
			leftTrigger = jsn ["left-trigger"];
			Debug.Log (leftTrigger);
			rightTrigger = jsn ["right-trigger"];
		}
	}

	public override float Horizontal {
		get {
			
			float thor = Input.GetAxis (lxAxis);
		
			return thor;
		}
	}

	public override float Vertical {
		get {
			
			float tver = Input.GetAxis (lyAxis);
			return tver;
		}
	}

	public override int HorizontalDown {
		get {
			float thor = Input.GetAxis (lxAxis);
			int horz = (thor != 0f && Mathf.Abs (thor) >= 0.1f) ? (int)Mathf.Sign (thor) : 0;
			return horz;
		}
	}

	public override int VerticalDown {
		get {
			float tver = Input.GetAxis (lyAxis);
			int vert = (tver != 0f && Mathf.Abs (tver) >= 0.1f) ? (int)Mathf.Sign (tver) : 0;
			return vert;
		}
	}

	private float RightHorizontal {
		get {
			float thor = Input.GetAxis (rxAxis);
			int horz = (thor != 0f && Mathf.Abs (thor) >= 0.5f) ? (int)Mathf.Sign (thor) : 0;
			return horz;
		}
	}

	private float RightVertical {
		get {
			float tver = Input.GetAxis (ryAxis);
			int vert = (tver != 0f && Mathf.Abs (tver) >= 0.1f) ? (int)Mathf.Sign (tver) : 0;
			return vert;
		}
	}
	public override bool GetKeyDown(int button) {
		KeyCode kc = (KeyCode)System.Enum.Parse (typeof(KeyCode), "JoystickButton" + button);
		return Input.GetKeyDown (kc);
	}

	public override bool IsEnterDown {
		get {
			return GetKeyDown (aButton);
		}
	}

	public override bool IsBackDown {
		get {
			return GetKeyDown (bButton);
		}
	}

	public override float PushForce {
		get {
			return Input.GetAxis (rightTrigger);
		}
	}

	public override float PullForce {
		get {
			float axVal = Input.GetAxis (leftTrigger);
			if (axVal != 0) {
				return (axVal + 1) / 2;
			} 
			return axVal;
		}
	}
	public override bool IsGamepad {
		get {
			return true;
		}
	}
		
	public override void RotateCamera ()
	{
		Camera cam = Camera.main;
	
		hAngle += RightHorizontal * maxRotSpeed * Time.deltaTime;
		vAngle = Mathf.Clamp (vAngle + (RightVertical * maxRotSpeed * Time.deltaTime), -maxYLook, maxYLook);

		Quaternion spinQuat = Quaternion.AngleAxis (hAngle, Vector3.up);
		Quaternion lookQuat = Quaternion.AngleAxis (vAngle, Vector3.right);
		cam.transform.rotation = spinQuat * lookQuat;
	}
}