using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureAudioController {

	float measureDuration;
	float startTime;
	float endTime;

	public event System.Action<float, float> OnFindSectionBounds;
	public event System.Action<float> OnUpdateSectionProgress;
	public event System.Action<float, float> OnStartAudioSection;
	public event System.Action OnEndSection;

	public MeasureAudioController (float songBPM) {
		measureDuration = 4 * 60 / songBPM;
	}

	public void FindSectionBounds (int measureIndex) {
		if (OnEndSection != null)
			OnEndSection ();
		startTime = measureDuration * (measureIndex - 0.25f);
		endTime = measureDuration * (measureIndex + 1.25f);
		if (OnFindSectionBounds != null)
			OnFindSectionBounds (startTime, endTime);
	}

	public void MonitorSectionProgress (float playheadPosition) {
		if (playheadPosition >= endTime && OnEndSection != null) {
			OnEndSection ();
		} else if (OnUpdateSectionProgress != null) {
			float progressRatio = (playheadPosition - startTime) / (endTime - startTime);
			OnUpdateSectionProgress (progressRatio);
		}
	}

	public void StartAudio () {
		if (OnStartAudioSection != null)
			OnStartAudioSection (startTime, endTime);
	}
}
