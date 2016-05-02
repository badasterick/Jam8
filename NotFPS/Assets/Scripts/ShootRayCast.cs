using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootRayCast : MonoBehaviour {

    public GameObject fPSController;
    public GameObject canvasObject;
	public GameObject laserholder;
    public Sprite[] canvasImages;
    private Image image;
    public float maxPowerDistance = 10.0f;
    public AudioClip pushSound;
    public AudioClip pullSound;
    private AudioSource m_AudioSource;
	private GameObject target = null;
    // Use this for initialization
    void Start ()
    {
        image = GameObject.Find("Canvas").GetComponentInChildren<Image>();
        //image.sprite = canvasImages[2];
        m_AudioSource = GetComponent<AudioSource>();
		laserholder.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		//drawlaser();
		LookForTarget();
        if (Input.GetMouseButtonDown(0))
        {
            //image.sprite = canvasImages[0];
            push();
			laserholder.SetActive (true);
            m_AudioSource.clip = pushSound;
            m_AudioSource.Play();
        }
        if (Input.GetMouseButtonDown(1))
        {
            //image.sprite = canvasImages[1];
            pull();
			laserholder.SetActive (true);
            m_AudioSource.clip = pullSound;
            m_AudioSource.Play();
        }
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            //image.sprite = canvasImages[2];
			laserholder.SetActive(false);
        }
    }

    void push()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd, out hit, maxPowerDistance))
        {
            if(hit.transform.tag.Equals("Metal"))
            {
                Debug.Log("hit " + hit.transform.tag);
                float massDifference = hit.transform.GetComponent<Rigidbody>().mass - fPSController.GetComponent<Rigidbody>().mass;
                if (massDifference < 0)
                {

                    hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * ((Mathf.Abs(massDifference)) * 200));
                }
                else
                {
                    fPSController.GetComponent<Rigidbody>().velocity += (-transform.forward.normalized * 200) / fPSController.GetComponent<Rigidbody>().mass;

                }
            }
        }
    }

    void pull() { 
		if (target != null) {
			float massDifference = target.GetComponent<Rigidbody> ().mass - fPSController.GetComponent<Rigidbody> ().mass;
			if (massDifference < 0) {
				target.GetComponent<Rigidbody> ().AddForce (-transform.forward.normalized * ((Mathf.Abs (massDifference)) * 200));
			} else {
				fPSController.GetComponent<Rigidbody> ().velocity += (transform.forward.normalized * 200) / fPSController.GetComponent<Rigidbody> ().mass;
			}
		}
    }

	void LookForTarget() {
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast (transform.position, fwd, out hit, maxPowerDistance)) {
			if (hit.transform.tag.Equals ("Metal") && target != hit.collider.gameObject) {
				if (target != null) {
					target.GetComponent<Renderer> ().material.color = new Color (1.0f, 165f / 255f, 0);
				}
				target = hit.collider.gameObject;
				target.GetComponent<Renderer> ().material.color = new Color (0, 0, 1.0f);
			} else if (target != hit.collider.gameObject && target != null) {
				target.GetComponent<Renderer> ().material.color = new Color (1.0f, 165f / 255f, 0);
				target = null;
			}
		} else if (target != null) {
			target.GetComponent<Renderer> ().material.color = new Color (1.0f, 165f / 255f, 0);;
			target = null;
		}
	}
}
