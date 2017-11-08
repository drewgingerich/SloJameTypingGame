using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManagerBehavior : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] IndicatorSpawnerBehavior indicatorSpawner;
	[SerializeField] TextViewBehavior textView;

	public event System.Action<float> OnEndPlay;

	PlayLoopManager playLoopManager;
	ScoreKeeper scoreKeeper;

	public void StartPlay (AudioClip song, BeatMap beatMap, string text) {
		Wire (beatMap, text);

		audioPlayer.OnUpdatePosition += PlayLoop;
		audioPlayer.LoadAudioClip (song);
		audioPlayer.PlayAudioSection (0, song.length);
	}

	void Wire (BeatMap beatMap, string text) {
		BeatMapReader mapReader = new BeatMapReader (beatMap);
		TextManager textManager = new TextManager ();

		BeatSpawner spawner = new BeatSpawner (mapReader);
		BeatTimeManager beatManager = new BeatTimeManager (spawner);
		BeatActivityMonitor activityMonitor = new BeatActivityMonitor (spawner);

		ScoringChecker scoringChecker = new ScoringChecker (textManager);
		scoreKeeper = new ScoreKeeper (activityMonitor, scoringChecker);
		TextIncrementManager textIncManager = new TextIncrementManager (activityMonitor, scoringChecker, textManager);

		playLoopManager = new PlayLoopManager (mapReader, beatManager, activityMonitor, scoringChecker);
		SessionEndMonitor endMonitor = new SessionEndMonitor (audioPlayer, mapReader, spawner, textManager);
		endMonitor.OnEndSession += EndPlay;

		indicatorSpawner.Wire (spawner);
		textView.Wire (textManager);

		textManager.LoadText (text);
	}
	
	void PlayLoop (float audioTime) {
		List<char> inputString = new List<char> (Input.inputString);
		playLoopManager.PlayLoop (audioTime, inputString);
	}

	void EndPlay () {
		float scorePercentage = scoreKeeper.GetScorePercentage ();
		if (OnEndPlay != null)
			OnEndPlay (scorePercentage);
	}
}
