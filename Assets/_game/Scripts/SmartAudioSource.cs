using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmartAudioSource : MonoBehaviour {

	public event System.Action OnLoadClip = delegate { };
	public event System.Action<float> OnChangeAudioTime = delegate { };
	public event System.Action OnPlay = delegate { };
	public event System.Action OnStop = delegate { };

	public AudioClip clip {
		get { return audioSource.clip; }
		set { audioSource.clip = value; }
	}

	[SerializeField] AudioSource audioSource;

	float endTime;
	float lastAudioTime;
	float smoothedAudioTime;

	public IEnumerator LoadClipAtPath(string path) {
		SongData song = DataNavigator.GetCurrentSongData();
		WWW www = new WWW("file://" + song.directoryPath + "/" + song.songTitle + ".wav");
		yield return www;
		audioSource.clip = www.GetAudioClip();
		OnLoadClip();
	}

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
		OnPlay();
	}

	public void Stop() {
		if (audioSource.isPlaying)
			audioSource.Stop();
		OnStop();
		gameObject.SetActive(false);
	}

	public void Pause() {
		if (audioSource.isPlaying)
			audioSource.Pause();
		gameObject.SetActive(false);
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
			OnStop();
			return;
		}

		if (!audioSource.isPlaying && smoothedAudioTime >=0) {
			audioSource.Play();
			audioSource.time = smoothedAudioTime;
		}
	}
}
