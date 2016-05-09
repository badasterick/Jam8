using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameEndTrigger : MonoBehaviour {

	public float waitTime = 2.0f;
	private bool triggered = false;

	void OnTriggerEnter() {
		if (!triggered) {
			triggered = true;
			Invoke ("EndGame", waitTime);
		}
	}
	void EndGame() {
		SceneManager.LoadScene ("YouWin");
	}
}
