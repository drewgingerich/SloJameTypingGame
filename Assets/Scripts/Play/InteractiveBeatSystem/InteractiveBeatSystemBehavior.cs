using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatSystemBehavior : MonoBehaviour {

	[SerializeField] PlaySettings playSettings;
	[SerializeField] AudioSource audioSource;
	[SerializeField] InteractiveBeatMap beatMap;
	[SerializeField] Passage passage;

	public PlayheadTracker PlayheadTracker { get; private set; }
	public InteractiveBeatScout BeatScout { get; private set; }
	public InteractiveBeatSpawner BeatSpawner { get; private set; }
	public InteractiveBeatTracker BeatTracker { get; private set; }
	public PassageReader PassageReader { get; private set; }
	public ScoreKeeper ScoreKeeper { get; private set; }

	void Awake () {
		float playheadStartingPosition = 0f;
		PlayheadTracker = new PlayheadTracker (playheadStartingPosition);
		BeatScout = new InteractiveBeatScout (playSettings.indicatorTravelTime, beatMap, PlayheadTracker);
		BeatSpawner = new InteractiveBeatSpawner (BeatScout, PlayheadTracker);
		BeatTracker = new InteractiveBeatTracker (BeatSpawner, playSettings.timingWindowHalfWidth);
		PassageReader = new PassageReader (passage);
		ScoreKeeper = new ScoreKeeper (BeatTracker, PassageReader);
		audioSource.Play ();
	}

	void Update () {
		PlayLoop ();
	}

	void PlayLoop () {
		PlayheadTracker.TrackPlayheadPosition (audioSource.time, Time.deltaTime);
		BeatScout.ScoutUpcomingBeats (PlayheadTracker.DeltaPlayheadPosition);
		ScoreKeeper.ScoreActiveBeats (Input.inputString);
	}
} 
