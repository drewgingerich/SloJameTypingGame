using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManagerBehavior : MonoBehaviour {

	[SerializeField] [TextArea] string text;

	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] IndicatorSpawnerBehavior indicatorSpawner;
	[SerializeField] TextViewBehavior textView;
	[SerializeField] HitView hitView;

	public event System.Action<float> OnEndPlay = delegate {};

	PlayLoopManager playLoopManager;
	ScoreKeeper scoreKeeper;

	public void Play () {
		StartCoroutine (InitializePlay ());
	}

	IEnumerator InitializePlay () {
		SongData song = DataNavigator.currentSong;
		WWW www = new WWW ("file://" + song.directoryPath + "/" + song.songTitle + ".wav");
		yield return www;

		AudioClip clip = www.GetAudioClip ();
		BeatMap map = new BeatMap (song, DataNavigator.beatmapIndex);
		Wire (map, text);

		audioPlayer.OnUpdatePosition += PlayLoop;
		audioPlayer.LoadClip (clip);
		audioPlayer.PlaySection (0, clip.length);
	}

	void Wire (BeatMap beatMap, string text) {
		BeatMapReader mapReader = new BeatMapReader (beatMap, text.Length);
		BeatSpawner spawner = new BeatSpawner (mapReader);
		BeatTimeManager beatManager = new BeatTimeManager (spawner);
		BeatActivityMonitor activityMonitor = new BeatActivityMonitor (spawner);

		TextManager textManager = new TextManager (activityMonitor);
		ScoringChecker scoringChecker = new ScoringChecker (textManager);
		scoreKeeper = new ScoreKeeper (activityMonitor, scoringChecker);

		playLoopManager = new PlayLoopManager (mapReader, beatManager, activityMonitor, scoringChecker);
		SessionEndMonitor endMonitor = new SessionEndMonitor (audioPlayer, mapReader, spawner, textManager);
		endMonitor.OnEndSession += EndPlay;

		indicatorSpawner.Wire (spawner);
		textView.Wire (textManager);
		hitView.Wire (activityMonitor, scoringChecker);

		textManager.LoadText (text);
	}

	void PlayLoop (float audioTime) {
		List<char> inputString = new List<char> (Input.inputString);
		playLoopManager.PlayLoop (audioTime, inputString);
	}

	void EndPlay () {
		float scorePercentage = scoreKeeper.GetScorePercentage ();
		OnEndPlay (scorePercentage);
	}
}
