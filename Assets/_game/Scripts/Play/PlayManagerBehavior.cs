using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManagerBehavior : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] IndicatorSpawnerBehavior indicatorSpawner;

	public event System.Action OnEndPlay;

	PlayLoopManager playLoopManager;

	public void StartPlay (AudioClip song, BeatMap beatMap, string text) {
		Wire (beatMap, text);

		audioPlayer.OnUpdatePosition += PlayLoop;
		audioPlayer.LoadAudioClip (song);
		audioPlayer.PlayAudioSection (0, song.length);
	}

	void Wire (BeatMap beatMap, string text) {
		BeatMapReader mapReader = new BeatMapReader (beatMap);
		BeatSpawner spawner = new BeatSpawner (mapReader);
		BeatTimeManager beatManager = new BeatTimeManager (spawner);
		BeatActivityMonitor activityMonitor = new BeatActivityMonitor (spawner);
		TextManager textManager = new TextManager (text, spawner);
		ScoringChecker scoringChecker = new ScoringChecker (textManager);
		ScoreKeeper scoreKeeper = new ScoreKeeper (activityMonitor, scoringChecker);
		playLoopManager = new PlayLoopManager (mapReader, beatManager, activityMonitor, scoringChecker);

		audioPlayer.OnEndSection += EndPlay;
		mapReader.OnFinishMap += EndPlay;
		textManager.OnTextEnd += EndPlay;

		indicatorSpawner.Wire (spawner);
	}
	
	void PlayLoop (float audioTime) {
		List<char> inputString = new List<char> (Input.inputString);
		playLoopManager.PlayLoop (audioTime, inputString);
	}

	void EndPlay () {
		if (OnEndPlay != null)
			OnEndPlay ();
	}
}
