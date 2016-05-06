using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intenity;
	public bool shake = false;
//	void OnGUI (){
//		if (GUI.Button (new Rect (20,40,80,20), "Shake")){
//			Shake ();
//		}
//	}
//
	void Update (){
		if (shake) {
			if (shake_intenity > 0) {
				//transform.position = originPosition + Random.insideUnitSphere * shake_intenity;
				transform.rotation = new Quaternion (
					originRotation.x + Random.Range (-shake_intenity, shake_intenity) * .1f,
					originRotation.y + Random.Range (-shake_intenity, shake_intenity) * .1f,
					originRotation.z + Random.Range (-shake_intenity, shake_intenity) * .1f,
					originRotation.w + Random.Range (-shake_intenity, shake_intenity) * .1f);
				//shake_intenity -= shake_decay;

			}
		}
	}

	public void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intenity = .9f;
		shake_decay = 0.002f;
	}
}