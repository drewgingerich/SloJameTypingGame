using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureAudioController {

	float measureDuration;
	float startTime;
	float endTime;

	public event System.Action<float, float> OnFindSectionBounds = delegate {};
	public event System.Action<float> OnUpdateSectionProgress = delegate {};
	public event System.Action<float, float> OnStartAudioSection = delegate {};
	public event System.Action OnEndSection = delegate {};

	public MeasureAudioController (float songBPM) {
		measureDuration = 4 * 60 / songBPM;
	}

	public void FindSectionBounds (int measureIndex) {
		OnEndSection ();
		startTime = measureDuration * (measureIndex - 0.25f);
		endTime = measureDuration * (measureIndex + 1.25f);
		OnFindSectionBounds (startTime, endTime);
	}

	public void MonitorSectionProgress (float playheadPosition) {
		if (playheadPosition >= endTime) {
			OnEndSection ();
		} else {
			float progressRatio = (playheadPosition - startTime) / (endTime - startTime);
			OnUpdateSectionProgress (progressRatio);
		}
	}

	public void StartAudio () {
		OnStartAudioSection (startTime, endTime);
	}
}
