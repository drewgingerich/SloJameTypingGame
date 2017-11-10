using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSectionPlayerController {

	public event System.Action OnStartSection;
	public event System.Action OnEndSection;
	public event System.Action<float> OnStartAudio {
		add { this.stateManager.OnStartAudio += value; }
		remove {this.stateManager.OnStartAudio -= value; }
	}
	public event System.Action OnEndAudio {
		add { this.stateManager.OnEndAudio += value; }
		remove {this.stateManager.OnEndAudio -= value; }
	}
	public event System.Action<float> OnUpdatePosition {
		add { this.tracker.OnUpdatePosition += value; }
		remove {this.tracker.OnUpdatePosition -= value; }
	}

	public bool paused { get; set; }

	PlayheadTracker tracker;
	AudioSectionStateManager stateManager;

	float sectionEndTime;

	public AudioSectionPlayerController (PlayheadTracker tracker, AudioSectionStateManager stateManager) {
		this.stateManager = stateManager;
		this.tracker = tracker;
		tracker.OnUpdatePosition += UpdatePositionState;
		paused = true;
	}

	public void SetClipLength (float clipLength) {
		stateManager.SetAudioEndTime (clipLength);
	}

	public void PlayAudioSection (float startTime, float endTime) {
		sectionEndTime = endTime;
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