using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuDesignScript : MonoBehaviour {


	public Sprite Untouched, Touched;
	public GameObject SpriteHolder, Credits, exit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 

		if (Physics.Raycast (ray, out hit, 100.0f)) {
			//StartCoroutine(ScaleMe(hit.transform));
			if (hit.collider.gameObject == this.gameObject) {
				SpriteHolder.gameObject.GetComponent<SpriteRenderer> ().sprite = Touched;
			} else {
				SpriteHolder.gameObject.GetComponent<SpriteRenderer> ().sprite = Untouched;
			}
		} 
		if ( Input.GetMouseButtonDown (0)){ 
			
			if ( Physics.Raycast (ray,out hit,100.0f)) {
				//StartCoroutine(ScaleMe(hit.transform));
				if (hit.collider.gameObject == this.gameObject) {
					Debug.Log ("You selected the " + hit.transform.name); // ensure you picked right object
					InvokeRepeating ("MoveDoor", 0f, 0.05f);
					Invoke ("LoadScene", 3f);
				}
				if (hit.collider.gameObject == exit.gameObject) {
					Debug.Log ("You selected the " + hit.transform.name); // ensure you picked right object
					//InvokeRepeating ("MoveDoor", 0f, 0.05f);
					Application.Quit();
				}
				if (hit.collider.gameObject == Credits.gameObject) {
					Debug.Log ("You selected the " + hit.transform.name); // ensure you picked right object
					//InvokeRepeating ("MoveDoor", 0f, 0.05f);
					SceneManager.LoadScene(2);
				}
			}
		}

	}

	void MoveDoor(){
		this.gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - 0.2f, gameObject.transform.position.z);

	}

	void LoadScene(){
		SceneManager.LoadScene(0);

	}
}

