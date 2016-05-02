using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	GenericController myController;
	Camera mainCam;
	private Vector3 velocity = Vector3.zero;
	private Vector3 totalForce = Vector3.zero;
	public float moveForce = 50;
	public float slowDown = 10;
	private float mass = 10;
	public float maxSpeed = 10;
	public float jumpSpeed = 5;
	public float gravity = 10;

	public GameObject laserholder;
	public ParticleSystem partSys;
	private float height;

	public float maxPowerDistance = 10.0f;
	public AudioClip pushSound;
	public AudioClip pullSound;
	private AudioSource m_AudioSource;
	private GameObject target = null;
	private Color orangeColor = new Color( 255f / 255f, 150f / 255f, 0);
	private Color lightBlueColor = new Color(0, 191f / 255f, 1f);

	private bool onGround = true;
	private Rigidbody myRigidBody;
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
		myRigidBody = GetComponent<Rigidbody> ();
		mainCam = Camera.main;
		height = GetComponent<CapsuleCollider>().bounds.size.y / 2;
		Debug.Log ("Height: " + height);
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		onGround = Physics.Raycast (transform.position, Vector3.down, out hit, height + float.Epsilon);
		myController.RotateCamera ();
		CheckForTarget ();
		totalForce = Vector3.zero;
		if (onGround) {
			float newY = hit.transform.position.y + hit.collider.bounds.size.y / 2 + height;
			transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			velocity = (myController.Vertical * Forward + myController.Horizontal * Right);




			velocity = velocity.normalized * maxSpeed;

			
			if (myController.IsJumpDown) {
				velocity.y = jumpSpeed;
			} else {
				velocity.y = 0;
			}
			


		} else {
			velocity.y -= gravity * Time.deltaTime;
		}

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
	
		if ((myController.PullForce <= 0 && myController.PushForce <= 0) || target == null) {
			laserholder.SetActive (false);
		}

		if (partSys.isPlaying) {

			ParticleSystem.Particle[] tempArr = new ParticleSystem.Particle[partSys.particleCount];
			partSys.GetParticles (tempArr);
			List<ParticleSystem.Particle> particles = tempArr.ToList();
			List<ParticleSystem.Particle> tempList = tempArr.ToList();
			Vector3 bdif = target.transform.position - transform.position;
			foreach (ParticleSystem.Particle p in tempArr) {
				Vector3 dif = p.position - transform.position;

				if (Vector3.Dot (dif, bdif.normalized) >= Vector3.Distance(transform.position, target.transform.position)) {
					
					particles.Remove (p);
				}
			}

			partSys.SetParticles (particles.ToArray (), particles.Count);

		}
		velocity += totalForce * Time.deltaTime / mass;
		ClampVelocity ();
		myRigidBody.velocity = velocity;
	}

	void AddForce(Vector3 force) {
		totalForce += force;
	}

	void ClampVelocity() {
		if (velocity.magnitude > maxSpeed) {
			velocity = velocity.normalized * maxSpeed;
		}
	}

	void CheckForTarget() {
		Vector3 fwd = Camera.main.transform.forward;
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit, maxPowerDistance)) {
			if (hit.collider.CompareTag ("Metal")) {
				GameObject hitTarget = hit.collider.gameObject;
			
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

	public Vector3 Forward {
		get {
			Vector3 fwd = Camera.main.transform.forward;
			fwd.y = 0;
			return fwd.normalized;
		}
	}

	public Vector3 Right {
		get {
			
			Vector3 rit = Camera.main.transform.right;
			rit.y = 0;
			return rit.normalized;
		}
	}
}
