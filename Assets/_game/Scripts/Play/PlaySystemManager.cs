using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySystemManager : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] PlayManagerBehavior playManager;
	[SerializeField] IndicatorSpawnerManager spawnerManager;
	[SerializeField] TextView textView;
	[SerializeField] HitView hitView;
	[SerializeField] new ParticleSystem particleSystem;

	[System.NonSerialized] public BeatCountTracker countTracker;
	[System.NonSerialized] public TextReader textKeeper;
	[System.NonSerialized] public BeatMapReader mapReader;
	[System.NonSerialized] public BeatSpawner spawner;
	[System.NonSerialized] public BeatManager beatManager;
	[System.NonSerialized] public BeatActivityMonitor activityMonitor;
	[System.NonSerialized] public ScoringChecker scoringChecker;
	[System.NonSerialized] public ScoreKeeper scoreKeeper;
	[System.NonSerialized] public PlayLoopManager playLoopManager;
	[System.NonSerialized] public SessionEndMonitor endMonitor;

	public void LoadSystem (SongData songData, List<float> beatMap, string text) {
		countTracker = new BeatCountTracker (songData);
		textKeeper = new TextReader (text);
		mapReader = new BeatMapReader(beatMap);
		spawner = new BeatSpawner(mapReader, textKeeper);
		beatManager = new BeatManager(spawner);
		activityMonitor = new BeatActivityMonitor(spawner);
		scoringChecker = new ScoringChecker();
		scoreKeeper = new ScoreKeeper(activityMonitor, scoringChecker);
		playLoopManager = new PlayLoopManager(countTracker, spawner, beatManager, activityMonitor, scoringChecker);
		endMonitor = new SessionEndMonitor(audioPlayer, mapReader, spawner, textKeeper);

		playManager.Wire(playLoopManager, scoreKeeper, endMonitor);
		spawnerManager.Wire(spawner);
		textView.Wire(text, scoringChecker, activityMonitor);
		hitView.Wire(activityMonitor, scoringChecker, particleSystem);
	}
}
