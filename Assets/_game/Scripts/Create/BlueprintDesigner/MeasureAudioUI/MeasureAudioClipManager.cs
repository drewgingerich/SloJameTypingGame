using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureAudioClipManager {

	float songDuration;
	float measureDuration;
	float startTime;
	float endTime;

	public event System.Action<float, float> OnSetClipBounds;
	public event System.Action<float> OnPlayAudio;
	public event System.Action<float, float> OnPlayAudioWithDelay;
	public event System.Action<float> OnUpdateClipProgress;
	public event System.Action OnStopAudio;

	public MeasureAudioClipManager (float songBPM, float songDuration) {
		this.songDuration = songDuration;
		measureDuration = 4 * 60 / songBPM;
	}

	public void SetClipBounds (int measureIndex, bool[] measure) {
		StopAudio ();
		startTime = measureDuration * (measureIndex - 0.25f);
		if (startTime > songDuration)
			startTime = songDuration;
		endTime = measureDuration * (measureIndex + 1.25f);
		if (endTime > songDuration)
			endTime = songDuration;
		if (OnSetClipBounds != null)
			OnSetClipBounds (startTime, endTime);
	}

	public void MonitorClipProgress (float playheadPosition) {
		if (playheadPosition >= endTime) {
			StopAudio ();
		} else {
			if (OnUpdateClipProgress != null)
				OnUpdateClipProgress ((playheadPosition - startTime) / (endTime - startTime));
		}
	}

	public void PlayAudio () {
		if (startTime >= 0 && OnPlayAudio != null)
			OnPlayAudio (startTime);
		else if (startTime < 0 && OnPlayAudioWithDelay != null)
			OnPlayAudioWithDelay (0, Mathf.Abs (startTime));
	}

	public void StopAudio () {
		if (OnStopAudio != null)
			OnStopAudio ();
	}
}
