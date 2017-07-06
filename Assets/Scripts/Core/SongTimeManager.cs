using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTimeManager : MonoBehaviour {

	[SerializeField] AudioSource audioSource;
	[SerializeField] RhythmSettings rhythmSettings;

	public event System.Action<float> OnReadPlayheadPosition;

	float totalPlayTime;
	float lastReportedPlayheadPosition;
	float interpolatedPlayheadPosition;

	void Start () {
		lastReportedPlayheadPosition = audioSource.time;
		interpolatedPlayheadPosition = rhythmSettings.indicatorTravelTime * -1 + rhythmSettings.visualOffset;
		totalPlayTime = rhythmSettings.indicatorTravelTime * -1 + rhythmSettings.visualOffset;
		OnReadPlayheadPosition += CheckForAudioPlay;

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
		if (OnReadPlayheadPosition != null) {
			OnReadPlayheadPosition (deltaSongTime);
		}
	}

	void CheckForAudioPlay (float deltaSongTime) {
		totalPlayTime += deltaSongTime;
		if (totalPlayTime >= 0) {
			audioSource.Play ();
			audioSource.time = totalPlayTime;
			OnReadPlayheadPosition -= CheckForAudioPlay;
		}
	}
}
