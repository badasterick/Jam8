using UnityEngine;
using System.Collections;

public class GenericController {
	protected int jumpKey;
	protected int interactKey;
	protected int pushKey;
	protected int pullKey;
	protected int swapKey;
	protected int backKey;
	protected int enterKey;
	public float maxRotSpeed = 90;
	public float maxYLook = 45;
	public virtual void LoadController() {
	}

	public virtual float Horizontal {
		get {
			return 0;
		}
	}

	public virtual int HorizontalDown {
		get {
			return 0;
		}
	}

	public virtual float Vertical {
		get {
			return 0;
		}
	}

	public virtual int VerticalDown {
		get {
			return 0;
		}
	}

	public virtual bool GetKeyDown(int button) {
		return false;
	}

	public virtual bool GetKeyDown(string button) {
		return false;
	}

	public virtual bool GetKey(string button) {
		return false;
	}

	public virtual bool IsEnterDown {
		get {
			return false;
		}
	}

	public virtual bool IsBackDown {
		get {
			return false;
		}
	}

	public virtual bool IsSwapDown {
		get {
			return GetKeyDown (swapKey);
		}
	}

	public virtual bool IsInteractDown {
		get {
			return GetKeyDown (interactKey);
		}
	}

	public virtual bool IsJumpDown {
		get {
			return GetKeyDown (jumpKey);
		}
	}

	public virtual bool IsGamepad {
		get {
			return false;
		}
	}

	public virtual bool UsesMouse {
		get {
			return false;
		}
	}

	public virtual float PullForce {
		get {
			return 0.0f;
		}
	}

	public virtual float PushForce {
		get {
			return 0.0f;
		}
	}

	public virtual void RotateCamera() {
	}
}