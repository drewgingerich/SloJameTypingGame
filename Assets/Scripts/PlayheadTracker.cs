using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadTracker : MonoBehaviour {

	[SerializeField] AudioSource audioSource;
	[SerializeField] PlaySettings playSettings;

	public event System.Action<float> OnReadPlayheadPosition;

	float totalPlayTime;
	float lastReportedPlayheadPosition;
	float interpolatedPlayheadPosition;

	void Start () {
		lastReportedPlayheadPosition = audioSource.time;
		interpolatedPlayheadPosition = playSettings.indicatorTravelTime * -1 + playSettings.visualOffset;
		totalPlayTime = playSettings.indicatorTravelTime * -1 + playSettings.visualOffset;
		OnReadPlayheadPosition += CheckForStartAudioPlay;
	}

	void Update () {
		float deltaSongTime;
		if (audioSource.time != lastReportedPlayheadPosition) {
			lastReportedPlayheadPosition = audioSource.time;
			deltaSongTime = audioSource.time - interpolatedPlayheadPosition;
			interpolatedPlayheadPosition = audioSource.time;
		} else {
			deltaSongTime = Time.deltaTime;
			interpolatedPlayheadPosition += Time.deltaTime;
		}
		if (OnReadPlayheadPosition != null) 
			OnReadPlayheadPosition (deltaSongTime);
	}

	void CheckForStartAudioPlay (float deltaSongTime) {
		totalPlayTime += deltaSongTime;
		if (totalPlayTime >= 0) {
			audioSource.Play ();
			audioSource.time = totalPlayTime;
			OnReadPlayheadPosition -= CheckForStartAudioPlay;
		}
	}
}
