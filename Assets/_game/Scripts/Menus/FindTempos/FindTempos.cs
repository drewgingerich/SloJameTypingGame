using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTempos : SnapMenu {

	[SerializeField] Text tempoText;
	[SerializeField] Text explanationText;
	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] Selectable firstSelected;

	bool audioReady;
	bool findingTempo;
	List<float> tapTimes;
	float searchStartTime;
	float bpm;

	public void SaveChanges() {
		SongData songData = DataNavigator.GetCurrentSongData();
		songData.bpm = bpm;
		songData.Save();
	}

	void OnEnable() {
		firstSelected.Select();
		audioReady = false;
		tempoText.text = "Tempo = ??? bpm";
		explanationText.text = "Loading song...";
		SongData songData = DataNavigator.GetCurrentSongData();
		StartCoroutine(LoadClip(songData.directoryPath + "/" + songData.songTitle + ".wav"));
	}

	IEnumerator LoadClip(string path) {
		WWW www = new WWW("file://" + path);
		yield return www;
		audioPlayer.LoadClip(www.GetAudioClip());
		audioReady = true;
		explanationText.text = "Press [SPACE] to start.";
	}

	void Update() {
		if (!audioReady)
			return;
		if (Input.GetKeyDown(KeyCode.Escape)) {
			EndTempoFinding();
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			if (!findingTempo) {
				StartTempoFinding();
				return;
			} else {
				UpdateBPM();
			}
		}
	}

	void UpdateBPM() {
		tapTimes.Add(Time.time);
		if (tapTimes.Count < 3)
			return;
		float intervalSum = 0;
		for (int i = 2; i < tapTimes.Count; i++)
			intervalSum += tapTimes[i] - tapTimes[i-1];
		float tempo = (tapTimes.Count - 2) / intervalSum;
		bpm = 60 * tempo;
		tempoText.text = string.Format("Tempo = {0:0.0} bpm", bpm);
	}
	void StartTempoFinding() {
		audioPlayer.Play();
		findingTempo = true;
		searchStartTime = Time.time;
		tapTimes = new List<float>();
		explanationText.text = "When you're ready, tap [SPACE] along with the rhythm. Press [ESC] to finish.";
	}

	void EndTempoFinding() {
		audioPlayer.Stop();
		findingTempo = false;
		explanationText.text = "Press [SPACE] to start the music.";
	}
}
