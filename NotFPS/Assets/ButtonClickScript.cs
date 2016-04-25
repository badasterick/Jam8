using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonClickScript : MonoBehaviour {
    
    public Button[] buttons;
    int timer = 1;
    bool falling = false;
	// Use this for initialization
	void Start () {
        falling = false;
	}
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (falling)
        {
            //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.1f, this.gameObject.transform.position.z);
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            timer += 1;
        }
        if(timer >= 20)
        {
            Application.Quit();
        }
	}

    void Removebuttons()
    {
        foreach(Button B in buttons)
        {
            Destroy(B.gameObject);

        }

    }
    public void ExitButton()
    {


        falling = true;
        Removebuttons();
    }
    public void StartButton()
    {
        Debug.Log("what");
        SceneManager.LoadScene(0);
        Removebuttons();
    }
    public void CreditsButton()
    {
        SceneManager.LoadScene(1);
        Removebuttons();
    }
}
