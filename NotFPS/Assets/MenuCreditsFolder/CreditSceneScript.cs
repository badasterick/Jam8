using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CreditSceneScript : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        Invoke("moveon", 7f);
	}
	
	// Update is called once per frame
	void Update () {

    }
    void moveon()
    {
        SceneManager.LoadScene(1);
    }
}
