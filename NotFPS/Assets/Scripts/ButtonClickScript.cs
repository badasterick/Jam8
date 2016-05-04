using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonClickScript : MonoBehaviour {

    public GameObject PlayButton;
    public Button[] buttons;
    int timer = 1;
    bool falling = false;
    bool starting = false;
	// Use this for initialization
	void Start () {
        falling = false;
        timer = 0;
	}
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);

    }
	
	// Update is called once per frame


    public void ExitButton()
    {
        Debug.Log("Exiting"); //exiting
        //Removebuttons();
        Application.Quit();
    }
    public void StartButton()
    {
        Debug.Log("what");
        starting = true;
        //PlayButton.GetComponent<ButtonPusher>().Push();
       SceneManager.LoadScene(0);
       
    }
    public void CreditsButton()
    {
        Debug.Log("Crediting");
        falling = true;
        SceneManager.LoadScene(2);
       
    }
}
