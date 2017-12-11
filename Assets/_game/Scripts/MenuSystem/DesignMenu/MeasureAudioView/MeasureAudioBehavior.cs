using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureAudioBehavior : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioSectionPlayer;
	[SerializeField] Button playButton;
	[SerializeField] Button stopButton;
	[SerializeField] Text timeText;

	public void Load (DesignMenuController designer, MeasureAudioController controller, string songTitle) {
		playButton.interactable = false;
		stopButton.interactable = false;

		designer.OnShiftMeasure += (measureIndex, _) => controller.FindSectionBounds (measureIndex);
		audioSectionPlayer.OnUpdatePosition += controller.MonitorSectionProgress;
		playButton.onClick.AddListener (controller.StartAudio);
		stopButton.onClick.AddListener (StopAudio);

		controller.OnFindSectionBounds += DisplaySectionBounds;
		controller.OnStartAudioSection += PlayAudio;
		controller.OnEndSection += StopAudio;

		StartCoroutine (LoadClip (SongDataManager.storagePath + songTitle + ".wav"));
	}

	IEnumerator LoadClip (string path) {
		WWW www = new WWW ("file://" + path);
		yield return www;
		audioSectionPlayer.LoadClip (www.GetAudioClip ());
		playButton.interactable = true;
	}

	void DisplaySectionBounds (float startTime, float endTime) {
		timeText.text = string.Format ("{0:0.000} - {1:0.000} sec", startTime, endTime);
	}

	void PlayAudio (float startTime, float endTime) {
		audioSectionPlayer.PlaySection (startTime, endTime);
		playButton.interactable = false;
		stopButton.interactable = true;
	}

	void StopAudio () {
		audioSectionPlayer.Stop ();
		playButton.interactable = true;
		stopButton.interactable = false;
	}
}
