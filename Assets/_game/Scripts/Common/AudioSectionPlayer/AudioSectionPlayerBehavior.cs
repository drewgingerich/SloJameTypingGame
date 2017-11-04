using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSectionPlayerBehavior : MonoBehaviour {

	[SerializeField] AudioSource audioSource;

	public event System.Action OnStartSection;
	public event System.Action OnEndSection;
	public event System.Action<float> OnStartAudio;
	public event System.Action OnEndAudio;
	public event System.Action<float> OnReadPosition;
	public event System.Action<float> OnChangePosition;

	AudioSectionPlayerController controller;

	public void LoadAudioClip (AudioClip clip) {
		audioSource.clip = clip;
		controller.SetClipLength (clip.length);
	}

	public void PlayAudioSection (float startTime, float endTime) {
		controller.PlayAudioSection (startTime, endTime);
	}

	public void Pause () {
		controller.paused = true;
	}

	public void Unpause () {
		controller.paused = false;
	}

	void Awake () {
		PlayheadTracker tracker = new PlayheadTracker ();
		AudioSectionStateManager stateManager = new AudioSectionStateManager ();
		controller = new AudioSectionPlayerController (tracker, stateManager);
	}

	void Update () {
		controller.MonitorSectionProgress (audioSource.time, Time.deltaTime);
	}
}
