using UnityEngine;
using System.Collections;

public class ShootRayCast : MonoBehaviour {

    public GameObject fPSController;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pushOrPullForLeftMouseButton();
        }
        if (Input.GetMouseButtonDown(1))
        {
            pushOrPullForRightMouseButton();
        }
    }

    void pushOrPullForLeftMouseButton()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position, fwd, out hit, float.MaxValue))
        {
            
            if(hit.transform.tag.Equals("Metal"))
            {
                Debug.Log("hit " + hit.transform.tag);
                if(hit.transform.GetComponent<Rigidbody>().mass < fPSController.GetComponent<Rigidbody>().mass)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                }
                else
                {
                    fPSController.GetComponent<Rigidbody>().transform.Translate(-transform.forward * 5);
                }
            }
        }
    }

    void pushOrPullForRightMouseButton()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, float.MaxValue))
        {

            if (hit.transform.tag.Equals("Metal"))
            {
                if (hit.transform.GetComponent<Rigidbody>().mass < fPSController.GetComponent<Rigidbody>().mass)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(-transform.forward * 500);
                }
                else
                {
                    fPSController.GetComponent<Rigidbody>().transform.Translate(transform.forward * 5);
                }
            }
        }
    }
}
