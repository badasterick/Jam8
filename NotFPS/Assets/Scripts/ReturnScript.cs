using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ReturnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReturnGam(){
		SceneManager.LoadScene (1);

	}
}
