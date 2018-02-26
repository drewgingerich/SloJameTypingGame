using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmartAudioSource : MonoBehaviour {

	public event System.Action<float> OnChangeAudioTime = delegate {};
	public event System.Action OnStart = delegate {};
	public event System.Action OnEnd = delegate {};

	public AudioClip clip { 
		get { return audioSource.clip; } 
		set { audioSource.clip = value; }
	}

	[SerializeField] AudioSource audioSource;

	float endTime;
	float lastAudioTime;
	float smoothedAudioTime;
	
	public void Play() {
		Play(0, clip.length);
	}

	public void Play(float startTime) {
		Play(startTime, clip.length);
	}

	public void Play(float startTime, float endTime) {
		gameObject.SetActive(true);
		if (audioSource.isPlaying)
			audioSource.Stop();
		if (startTime >= 0 && startTime < audioSource.clip.length) {
			audioSource.Play();
			audioSource.time = lastAudioTime = smoothedAudioTime = startTime;
		} else {
			audioSource.time = lastAudioTime = 0;
			smoothedAudioTime = startTime;
		}
		this.endTime = endTime;
		OnStart();
	}

	public void Stop() {
		gameObject.SetActive(false);
		if (audioSource.isPlaying)
			audioSource.Stop();
	}

	public void Pause() {
		gameObject.SetActive(false);
		if (audioSource.isPlaying)
			audioSource.Pause();
	}

	public void Unpause() {
		gameObject.SetActive(true);
		audioSource.UnPause();
	}

	void Update() {
		if (audioSource.time != lastAudioTime)
			lastAudioTime = smoothedAudioTime = audioSource.time;
		else
			smoothedAudioTime += Time.deltaTime;
		OnChangeAudioTime(smoothedAudioTime);

		if (smoothedAudioTime >= endTime) {
			Stop();
			OnEnd();
			return;
		}

		if (!audioSource.isPlaying && smoothedAudioTime >=0) {
			audioSource.Play();
			audioSource.time = smoothedAudioTime;
		}
	}
}
