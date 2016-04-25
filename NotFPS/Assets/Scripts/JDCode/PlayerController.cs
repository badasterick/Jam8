using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	GenericController myController;
	Camera mainCam;
	private float yVel = 0;

	public GameObject laserholder;

	public float maxPowerDistance = 10.0f;
	public AudioClip pushSound;
	public AudioClip pullSound;
	private AudioSource m_AudioSource;
	// Use this for initialization
	void Start () {
		if (Input.GetJoystickNames ().Length > 0) {
			Debug.Log("There's a controller");
			myController = new GamepadController ();
		} else {
			myController = new KeyboardController ();
		}

		myController.LoadController ();
		mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		myController.RotateCamera ();
		Vector3 vel = ((mainCam.transform.forward * myController.Vertical) + (mainCam.transform.right * myController.Horizontal));
		transform.position += new Vector3 (vel.x, 0, vel.z).normalized * 20 * Time.deltaTime;

		if (myController.PushForce > 0)
		{
			push();
			laserholder.SetActive (true);
			//m_AudioSource.clip = pushSound;
			//m_AudioSource.Play();
		}
		if (myController.PullForce > 0)
		{
			pull();
			laserholder.SetActive (true);
			//m_AudioSource.clip = pullSound;
			//m_AudioSource.Play();
		}
		if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			laserholder.SetActive(false);
		}
	}

	void push()
	{
		RaycastHit hit;
		Vector3 fwd = Camera.main.transform.forward;
		if(Physics.Raycast(transform.position, fwd, out hit, maxPowerDistance))
		{
			if(hit.transform.tag.Equals("Metal"))
			{
				Debug.Log("hit " + hit.transform.tag);
				//float massDifference = hit.transform.GetComponent<Rigidbody>().mass - fPSController.GetComponent<Rigidbody>().mass;
				if (hit.transform.GetComponent<Rigidbody>().mass > 1)
				{
					hit.transform.GetComponent<Rigidbody>().AddForce(fwd.normalized * 200);
					//hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * ((Mathf.Abs(massDifference)) * 200));
				}
				else
				{
					//fPSController.GetComponent<Rigidbody>().velocity += (-transform.forward.normalized * 200) / fPSController.GetComponent<Rigidbody>().mass;

				}
			}
		}
	}

	void pull() { 
		RaycastHit hit;
		Vector3 fwd = Camera.main.transform.forward;
		if (Physics.Raycast(transform.position, fwd, out hit, maxPowerDistance))
		{
			if (hit.transform.tag.Equals("Metal"))
			{
				//float massDifference = hit.transform.GetComponent<Rigidbody>().mass - fPSController.GetComponent<Rigidbody>().mass;
				if (hit.transform.GetComponent<Rigidbody>().mass > 1)
				{
					hit.transform.GetComponent<Rigidbody>().AddForce(-fwd.normalized * 200);
					//hit.transform.GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * ((Mathf.Abs(massDifference)) * 200));
				}
				else
				{
					//fPSController.GetComponent<Rigidbody>().velocity += (transform.forward.normalized * 200) / fPSController.GetComponent<Rigidbody>().mass;
				}
			}
		}
	}
}
