using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class YouWinScreen : MonoBehaviour {

	void Start() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	public void BackToMainMenu() {
		SceneManager.LoadScene ("MenuDesignTest");
	}
}
