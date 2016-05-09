using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ResetScript : MonoBehaviour {
	public AudioClip bgm;
	public GameObject lastCheckpoint;

	// Use this for initialization
	void Start () 
	{
		AudioManager.Instance.PlayBGM (bgm);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.transform.position.y < 0.4) 
		{
			ResetPosition ();
		}
	}
		
	public void ResetPosition()
	{
		CheckpointUpdateScript checkpoint = lastCheckpoint.GetComponent<CheckpointUpdateScript> ();
		transform.position = checkpoint.transform.position;
		GetComponent<RigidbodyFirstPersonController> ().ResetPlayer (checkpoint.forward);
	}
}