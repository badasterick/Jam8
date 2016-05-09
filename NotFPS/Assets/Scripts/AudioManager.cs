using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	private AudioSource bgmSource;
	// Use this for initialization
	void Awake () {
		AudioManager.instance = this;
		bgmSource = GetComponent<AudioSource> ();
	}

	public void PlayBGM(AudioClip ac) {
		bgmSource.Stop ();
		bgmSource.clip = ac;
		bgmSource.Play ();
	}

	public void StopBGM() {
		bgmSource.Stop ();
	}

	public void PauseBGM() {
		bgmSource.Pause ();
	}

	public void UnpauseBGM() {
		bgmSource.UnPause ();
	}

	public void RestartBGM() {
		bgmSource.Stop ();
		bgmSource.Play ();
	}
		
	public float BGMVolume {
		get {
			return bgmSource.volume;
		}

		set {
			bgmSource.volume = value;
		}
	}

	public bool LoopBGM {
		get {
			return bgmSource.loop;
		}

		set {
			bgmSource.loop = value;
		}
	}
	public bool IsBGMPlaying {
		get {
			return bgmSource.isPlaying;
		}
	}

	public float BGMPlaybackTime {
		get {
			return bgmSource.time;
		}

		set {
			bgmSource.time = value;
		}
	}

	public static AudioManager Instance {
		get {
			return AudioManager.instance;
		}
	}
}
