using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour {

	public UnityEvent OnEndPlay;

	[SerializeField] [TextArea] string text;
	[SerializeField] GameManager gameManager;
	[SerializeField] SmartAudioSource audioPlayer;
	[SerializeField] PlaySystemManager systemManager;

	PlayLoopManager playLoopManager;
	ScoreKeeper scoreKeeper;

	public void Wire(PlayLoopManager loopManager, ScoreKeeper scoreKeeper, SessionEndMonitor endMonitor) {
		playLoopManager = loopManager;
		this.scoreKeeper = scoreKeeper;
		endMonitor.OnEndSession += EndPlay;
	}

	void Awake () {
		gameObject.SetActive (false);
	}

	void Start() {
		audioPlayer.OnChangeAudioTime += PlayLoop;
	}

	void OnEnable() {
		SongData song = DataNavigator.GetCurrentSongData();
		BeatmapBlueprint blueprint = DataNavigator.GetCurrentBlueprint();
		List<float> beatMap = blueprint.GetTargetCounts();
		systemManager.LoadSystem(song, beatMap, text);
		audioPlayer.OnLoadClip += StartPlay;
		StartCoroutine(audioPlayer.LoadClipAtPath("file://" + song.directoryPath + "/" + song.songTitle + ".wav"));
	}

	void OnDisable() {
		audioPlayer.OnLoadClip -= StartPlay;
	}
	void StartPlay() {
		audioPlayer.Play(-5f);
	}

	void PlayLoop(float audioTime) {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			EndPlay();
			return;
		}
		List<char> inputString = new List<char>(Input.inputString);
		playLoopManager.PlayLoop(audioTime, inputString);
	}

	void EndPlay() {
		StartCoroutine(EndPlayRoutine());
	}

	IEnumerator EndPlayRoutine() {
		yield return new WaitForSecondsRealtime(1);
		float scorePercentage = scoreKeeper.GetScorePercentage();
		audioPlayer.Stop();
		OnEndPlay.Invoke();
	}
}
