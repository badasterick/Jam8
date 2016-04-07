using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootRayCast : MonoBehaviour {

    public GameObject fPSController;
    public GameObject canvasObject;
    public Sprite[] canvasImages;
    private Image image;
    public float maxPowerDistance = 10.0f;
    public AudioClip pushSound;
    public AudioClip pullSound;
    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start ()
    {
        image = GameObject.Find("Canvas").GetComponentInChildren<Image>();
        image.sprite = canvasImages[2];
        m_AudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            image.sprite = canvasImages[0];
            push();
            m_AudioSource.clip = pushSound;
            m_AudioSource.Play();
        }
        if (Input.GetMouseButtonDown(1))
        {
            image.sprite = canvasImages[1];
            pull();
            m_AudioSource.clip = pullSound;
            m_AudioSource.Play();
        }
        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            image.sprite = canvasImages[2];
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
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, maxPowerDistance))
        {

            if (hit.transform.tag.Equals("Metal"))
            {
                float massDifference = hit.transform.GetComponent<Rigidbody>().mass - fPSController.GetComponent<Rigidbody>().mass;
                if (massDifference < 0)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * ((Mathf.Abs(massDifference)) * 200));
                }
                else
                {
                    fPSController.GetComponent<Rigidbody>().velocity += (transform.forward.normalized * 200) / fPSController.GetComponent<Rigidbody>().mass;
                }
            }
        }
    }
}
