using UnityEngine;
using System.Collections;

public class ButtonPusher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Push()
    {
        this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(2500f, this.gameObject.transform.forward, 50f);
    }
}
