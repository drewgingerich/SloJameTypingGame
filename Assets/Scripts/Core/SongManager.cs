using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

	[SerializeField] AudioSource audioSource;
	[SerializeField] SongBeats songBeats;
	[SerializeField] RhythmSettings rhythmSettings;

	public event System.Action<float> OnReadPlayheadPosition;

	float lastReportedPlayheadPosition;
	float interpolatedPlayheadPosition;

	void Start () {
		lastReportedPlayheadPosition = audioSource.time;
		interpolatedPlayheadPosition = rhythmSettings.indicatorTravelTime * -1 + rhythmSettings.visualOffset;
		audioSource.PlayDelayed (rhythmSettings.indicatorTravelTime);
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
}
