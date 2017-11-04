using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSectionPlayerController {

	public event System.Action OnStartSection;
	public event System.Action OnEndSection;
	public event System.Action<float> OnStartAudio;
	public event System.Action OnEndAudio;
	public event System.Action<float> OnReadPosition;
	public event System.Action<float> OnChangePosition;

	public bool paused { get; set; }

	PlayheadTracker tracker;
	AudioSectionStateManager stateManager;

	float sectionStartTime;
	float sectionEndTime;

	public AudioSectionPlayerController (PlayheadTracker tracker, AudioSectionStateManager stateManager) {
		this.stateManager = stateManager;
		stateManager.OnStartAudio += (position) => {
			if (OnStartAudio != null) OnStartAudio (position);
		};
		stateManager.OnEndAudio += () => {
			if (OnEndAudio != null) OnEndAudio ();
		};
		this.tracker = tracker;
		tracker.OnReadPosition += (position) => {
			UpdatePositionState (position);
			if (OnReadPosition != null) OnReadPosition (position);
		};
		tracker.OnChangePosition += (change) => {
			if (OnChangePosition != null) OnChangePosition (change);
		};
		paused = true;
	}

	public void SetClipLength (float clipLength) {
		stateManager.SetAudioEndTime (clipLength);
	}

	public void PlayAudioSection (float startTime, float endTime) {
		this.sectionStartTime = startTime;
		this.sectionEndTime = endTime;
		stateManager.InitializeStateManagment (startTime);
		tracker.InitializeTracking (startTime);
		StartSection ();
	}

	public void MonitorSectionProgress (float reportedPosition, float timeChange) {
		if (paused)
			return;
		if (stateManager.InAudioState ())
			tracker.TrackByReportedPosition (reportedPosition, timeChange);
		else
			tracker.TrackByTimeChange (timeChange);
	}

	void StartSection () {
		paused = false;
		if (OnStartSection != null) OnStartSection ();
	}

	void EndSection () {
		paused = true;
		if (OnEndSection != null) OnEndSection ();
	}

	void UpdatePositionState (float position) {
		if (position >= sectionEndTime)
			EndSection ();
		stateManager.UpdateState (position);
	}
}