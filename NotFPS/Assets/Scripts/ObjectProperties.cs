using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
