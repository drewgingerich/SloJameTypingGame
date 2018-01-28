using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySystemManager : MonoBehaviour {

	[SerializeField] AudioSectionPlayerBehavior audioPlayer;
	[SerializeField] IndicatorSpawner indicatorSpawner;
	[SerializeField] TextViewBehavior textView;
	[SerializeField] HitView hitView;

	[System.NonSerialized] public BeatMapReader mapReader;
	[System.NonSerialized] public BeatSpawner spawner;
	[System.NonSerialized] public BeatTimeManager beatManager;
	[System.NonSerialized] public BeatActivityMonitor activityMonitor;
	[System.NonSerialized] public TextManager textManager;
	[System.NonSerialized] public ScoringChecker scoringChecker;
	[System.NonSerialized] public ScoreKeeper scoreKeeper;
	[System.NonSerialized] public PlayLoopManager playLoopManager;
	[System.NonSerialized] public SessionEndMonitor endMonitor;

	public void LoadSystem (BeatMap beatMap, string text) {
		mapReader = new BeatMapReader(beatMap, text.Length);
		spawner = new BeatSpawner(mapReader);
		beatManager = new BeatTimeManager(spawner);
		activityMonitor = new BeatActivityMonitor(spawner);
		textManager = new TextManager(activityMonitor);
		scoringChecker = new ScoringChecker(textManager);
		scoreKeeper = new ScoreKeeper(activityMonitor, scoringChecker);
		playLoopManager = new PlayLoopManager(mapReader, beatManager, activityMonitor, scoringChecker);
		endMonitor = new SessionEndMonitor(audioPlayer, mapReader, spawner, textManager);

		indicatorSpawner.Wire(spawner);
		textView.Wire(textManager);
		hitView.Wire(activityMonitor, scoringChecker);

		textManager.LoadText(text);
	}
}
