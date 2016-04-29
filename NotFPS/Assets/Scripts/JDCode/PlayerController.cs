using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	GenericController myController;
	Camera mainCam;
	private float yVel = 0;
	private Vector3 velocity = Vector3.zero;
	private Vector3 totalForce = Vector3.zero;
	public float moveForce = 50;
	public float slowDown = 10;
	private float mass = 10;
	public float maxSpeed = 10;
	public float minSpeedBeforeStopping = 0.01f;
	public GameObject laserholder;
	public ParticleSystem partSys;

	public float maxPowerDistance = 10.0f;
	public AudioClip pushSound;
	public AudioClip pullSound;
	private AudioSource m_AudioSource;
	private GameObject target = null;
	private Color orangeColor = new Color( 255f / 255f, 150f / 255f, 0);
	private Color lightBlueColor = new Color(0, 191f / 255f, 1f);
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		partSys = laserholder.GetComponent<ParticleSystem> ();
		partSys.startSpeed = 50;
		laserholder.SetActive (false);
		if (Input.GetJoystickNames ().Length > 0) {
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
		CheckForTarget ();
		totalForce = Vector3.zero;
		Vector3 hvForce = (mainCam.transform.forward * myController.Vertical) + (mainCam.transform.right * myController.Horizontal);
		hvForce.y = 0;

		if (hvForce.magnitude == 0) {
			hvForce = -velocity.normalized * slowDown;
		}
		AddForce (hvForce.normalized * moveForce);


		//Vector3 vel = ((mainCam.transform.forward * myController.Vertical) + (mainCam.transform.right * myController.Horizontal));
		//transform.position += new Vector3 (vel.x, 0, vel.z).normalized * 20 * Time.deltaTime;

		if (myController.PushForce > 0 && target != null)
		{
			
			push();
			laserholder.SetActive (true);
			//m_AudioSource.clip = pushSound;
			//m_AudioSource.Play();
		}

		if (myController.PullForce > 0 && target != null)
		{
			pull();
			laserholder.SetActive (true);

			//m_AudioSource.clip = pullSound;
			//m_AudioSource.Play();
		}
	
		if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || target == null)
		{
			laserholder.SetActive(false);
		}

		velocity += totalForce * Time.deltaTime / mass;
		ClampVelocity ();
		transform.position += velocity * Time.deltaTime;
	}

	void AddForce(Vector3 force) {
		totalForce += force;
	}

	void ClampVelocity() {
		if (velocity.magnitude > maxSpeed) {
			velocity = velocity.normalized * maxSpeed;
		} else if (velocity.magnitude < minSpeedBeforeStopping) {
			velocity = Vector3.zero;
		}
	}

	void CheckForTarget() {
		Vector3 fwd = Camera.main.transform.forward;
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit, maxPowerDistance)) {
			if (hit.collider.CompareTag ("Metal")) {
				GameObject hitTarget = hit.collider.gameObject;
				float twoFiveFive = 255f;
				if (hitTarget != target) {
					if (target != null) {
						target.GetComponent<Renderer> ().material.color = orangeColor;
					}
					partSys.startLifetime = partSys.startSpeed / hit.distance;
					hitTarget.GetComponent<Renderer> ().material.color = lightBlueColor;
					target = hitTarget;
				}
			} else if (target != null) {
				target.GetComponent<Renderer> ().material.color = orangeColor;
				target = null;
			}
		} else if (target != null) {
			target.GetComponent<Renderer> ().material.color = orangeColor;
			target = null;
		}
	}
	void push()
	{
		if (target != null) {
			Vector3 fwd = Camera.main.transform.forward.normalized;
			Rigidbody targetBody = target.GetComponent<Rigidbody> ();
			float targetMass = targetBody.mass;

			if (targetMass < mass) {
				
				targetBody.AddForce (fwd * 200);
			} else {
				AddForce (-fwd * 200);
			}
		}
		/*
		if(Physics.Raycast(transform.position, fwd, out hit, maxPowerDistance))
		{
			if(hit.transform.tag.Equals("Metal"))
			{
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
		}*/
	}

	void pull() { 
		if (target != null) {
			Vector3 fwd = Camera.main.transform.forward.normalized;
			Rigidbody targetBody = target.GetComponent<Rigidbody> ();
			float targetMass = targetBody.mass;
			if (targetMass < mass) {
				
				targetBody.AddForce (-fwd * 200);
			} else {
				AddForce (fwd * 200);
			}
		}
		/*
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
		}*/
	}
}
