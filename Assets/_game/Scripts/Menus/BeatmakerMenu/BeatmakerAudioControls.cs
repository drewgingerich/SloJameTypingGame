using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable] public class UpdateSectionProgressEvent : UnityEvent<float> { }
[System.Serializable] public class FindSectionBoundsEvent : UnityEvent<float, float> { }

public class BeatmakerAudioControls : MonoBehaviour {

	public UnityEvent OnStartLoading;
	public UnityEvent OnStartPlaying;
	public UnityEvent OnReady;
	public FindSectionBoundsEvent OnFindSectionBounds;
	public UpdateSectionProgressEvent OnUpdateSectionProgress;

	[SerializeField] SmartAudioSource audioSource;

	float measureDuration;
	float startTime;
	float endTime;

	void OnEnable() {
		OnStartLoading.Invoke();
		SongData songData = DataNavigator.GetCurrentSongData();
		measureDuration = 4 * 60 / songData.bpm;
		audioSource.OnChangeAudioTime += TrackSectionProgress;
		audioSource.OnStop += Cleanup;
		StartCoroutine(audioSource.LoadClipAtPath(songData.directoryPath + "/" + songData.songTitle + ".wav"));
	}

	void OnDisable() {
		audioSource.OnChangeAudioTime -= TrackSectionProgress;
		audioSource.OnStop -= Cleanup;
	}

	public void FindSectionBounds(int measureIndex) {
		startTime = measureDuration * (measureIndex - 0.25f);
		endTime = measureDuration * (measureIndex + 1.25f);
		OnFindSectionBounds.Invoke(startTime, endTime);
	}

	public void Play() {
		audioSource.Play(startTime, endTime);
		OnStartPlaying.Invoke();
	}

	public void Stop() {
		audioSource.Stop();
		Cleanup();
	}

	void Cleanup() {
		OnReady.Invoke();
		OnUpdateSectionProgress.Invoke(0f);
	}

	void TrackSectionProgress(float audioTime) {
		float progressRatio = (audioTime - startTime) / (endTime - startTime);
		OnUpdateSectionProgress.Invoke(progressRatio);
	}
}
