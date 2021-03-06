﻿// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AudioSectionPlayerBehavior : MonoBehaviour {

// 	[SerializeField] AudioSource audioSource;

// 	public event System.Action OnStartSection {
// 		add { this.controller.OnStartSection += value; }
// 		remove {this.controller.OnStartSection -= value; }
// 	}
// 	public event System.Action OnEndSection {
// 		add { this.controller.OnEndSection += value; }
// 		remove {this.controller.OnEndSection -= value; }
// 	}
// 	public event System.Action<float> OnStartAudio {
// 		add { this.controller.OnStartAudio += value; }
// 		remove {this.controller.OnStartAudio -= value; }
// 	}
// 	public event System.Action OnEndAudio {
// 		add { this.controller.OnEndAudio += value; }
// 		remove {this.controller.OnEndAudio -= value; }
// 	}
// 	public event System.Action<float> OnUpdatePosition {
// 		add { this.controller.OnUpdatePosition += value; }
// 		remove {this.controller.OnUpdatePosition -= value; }
// 	}

// 	AudioSectionPlayerController controller;

// 	public void LoadClip (AudioClip clip) {
// 		audioSource.clip = clip;
// 		controller.SetClipLength (clip.length);
// 	}

// 	public void PlaySection (float startTime, float endTime) {
// 		controller.PlayAudioSection (startTime, endTime);
// 	}

// 	public void Play() {
// 		if (audioSource.clip == null)
// 			return;
// 		controller.PlayAudioSection (0f, audioSource.clip.length);
// 	}

// 	public void Stop () {
// 		controller.paused = true;
// 		audioSource.Stop ();
// 	}

// 	public void Pause () {
// 		controller.paused = true;
// 		audioSource.Pause ();
// 	}

// 	public void Unpause () {
// 		controller.paused = false;
// 		audioSource.UnPause ();
// 	}

// 	void StartAudio (float startTime) {
// 		audioSource.Play ();
// 		audioSource.time = startTime;
// 	}

// 	void Awake () {
// 		PlayheadTracker tracker = new PlayheadTracker ();
// 		AudioSectionStateManager stateManager = new AudioSectionStateManager ();
// 		controller = new AudioSectionPlayerController (tracker, stateManager);
// 		controller.OnStartAudio += StartAudio;
// 	}

// 	void Update () {
// 		controller.MonitorSectionProgress (audioSource.time, Time.deltaTime);
// 	}
// }
