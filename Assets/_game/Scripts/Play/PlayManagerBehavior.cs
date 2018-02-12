using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManagerBehavior : MonoBehaviour {

	public event System.Action<float> OnEndPlay = delegate { };

	[SerializeField] [TextArea] string text;
	[SerializeField] GameManagerBehavior gameManager;
	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] PlaySystemManager systemManager;

	PlayLoopManager playLoopManager;
	ScoreKeeper scoreKeeper;

	public void Wire (PlayLoopManager loopManager, ScoreKeeper scoreKeeper, SessionEndMonitor endMonitor) {
		playLoopManager = loopManager;
		this.scoreKeeper = scoreKeeper;
		endMonitor.OnEndSession += EndPlay;
	}

	public void Play () {
		SongData song = DataNavigator.GetCurrentSongData ();
		BeatmapBlueprint blueprint = song.blueprints[DataNavigator.beatmapIndex];
		List<float> beatMap = blueprint.GetTargetCounts ();
		systemManager.LoadSystem (song, beatMap, text);
		StartCoroutine (LoadMusic ());
	}

	IEnumerator LoadMusic () {
		SongData song = DataNavigator.GetCurrentSongData ();
		WWW www = new WWW ("file://" + song.directoryPath + "/" + song.songTitle + ".wav");
		yield return www;

		AudioClip clip = www.GetAudioClip ();
		audioPlayer.OnUpdatePosition += PlayLoop;
		audioPlayer.LoadClip (clip);
		audioPlayer.PlaySection (0, clip.length);
	}

	void PlayLoop (float audioTime) {
		List<char> inputString = new List<char> (Input.inputString);
		playLoopManager.PlayLoop (audioTime, inputString);
	}

	void EndPlay () {
		StartCoroutine (Cleanup ());
	}

	IEnumerator Cleanup () {
		yield return new WaitForSecondsRealtime (1);
		float scorePercentage = scoreKeeper.GetScorePercentage ();
		audioPlayer.Stop ();
		OnEndPlay (scorePercentage);
		gameManager.LoadStats (scoreKeeper.totalNumberBeats, scoreKeeper.beatsHit, scoreKeeper.GetScorePercentage ());
	}
}
