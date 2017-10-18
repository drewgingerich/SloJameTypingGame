using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureAudioViewBehavior : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioSectionManager;
	[SerializeField] Button playButton;
	[SerializeField] Button stopButton;
	[SerializeField] Text timeText;

	public void Wire (BlueprintDesigner designer, MeasureAudioClipManager clipManager, string songTitle) {
		playButton.interactable = false;
		stopButton.interactable = false;

		designer.OnShiftMeasure += clipManager.SetClipBounds;
        audioSectionManager.OnReadPosition += clipManager.MonitorClipProgress;
        playButton.onClick.AddListener ( () => clipManager.PlayAudio () );
        stopButton.onClick.AddListener ( () => clipManager.StopAudio () );

		clipManager.OnStopAudio += StopAudio;
		clipManager.OnPlayAudio += PlayAudio;
		clipManager.OnPlayAudioWithDelay += PlayAudioWithDelay;
		clipManager.OnSetClipBounds += UpdateClipTimes;

		StartCoroutine (LoadClip (SongImportManager.storagePath + songTitle + ".wav"));
	}

	IEnumerator LoadClip (string path) {
		WWW www = new WWW ("file://" + path);
		yield return www;
		//audioSectionManager.LoadClip (www.GetAudioClip ());
		playButton.interactable = true;
	}

	void UpdateClipTimes (float startTime, float endTime) {
		timeText.text = string.Format ("{0:0.000} - {1:0.000} sec", startTime, endTime);
	}

	void PlayAudio (float startTime) {
		//audioSectionManager.StartAudio (startTime);
		playButton.interactable = false;
		stopButton.interactable = true;
	}

	void PlayAudioWithDelay (float startTime, float delay) {
		// audioSectionManager.StartAudio (startTime, delay);
		playButton.interactable = false;
		stopButton.interactable = true;
	}

	void StopAudio () {
		// audioSectionManager.StopAudio ();
		playButton.interactable = true;
		stopButton.interactable = false;
	}
}
