using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatSystemBehavior : MonoBehaviour {

	[SerializeField] AudioClip audioClip;
	[SerializeField] PlaySettings playSettings;
	[SerializeField] AudioSectionPlayerBehavior audioManager;
	[SerializeField] SongInfo songInfo;
	[SerializeField] Passage passage;

	public AudioSectionPlayerBehavior PlayheadMonitor { get; private set; }
	public BeatMapReader BeatScout { get; private set; }
	public InteractiveBeatSpawner BeatSpawner { get; private set; }
	public InteractiveBeatTracker BeatTracker { get; private set; }
	public PassageReader PassageReader { get; private set; }
	public ScoreKeeper ScoreKeeper { get; private set; }

	void Awake () {
		SongTimeBeatMap beatTimings = new SongTimeBeatMap (songInfo, 1);
		BeatScout = new BeatMapReader (playSettings.indicatorTravelTime, beatTimings, PlayheadMonitor);
		BeatSpawner = new InteractiveBeatSpawner (BeatScout, PlayheadMonitor);
		BeatTracker = new InteractiveBeatTracker (BeatSpawner, playSettings.timingWindowHalfWidth);
		PassageReader = new PassageReader (passage);
		ScoreKeeper = new ScoreKeeper (BeatTracker, PassageReader);
	}

	void Start () {
		//audioManager.LoadClip (audioClip);
		float playheadStartingPosition = 0f;
		//audioManager.StartAudio (playheadStartingPosition);
	}

	void Update () {
		//PlayheadMonitor.MonitorPlayheadPosition (audioSource.time, Time.deltaTime);
		ScoreKeeper.ScoreActiveBeats (Input.inputString);
	}
} 
