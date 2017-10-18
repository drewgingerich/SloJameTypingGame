using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintDesignerViewBehavior : MonoBehaviour {

	[SerializeField] Button backButton;
	[SerializeField] Button saveButton;
	[SerializeField] MeasureViewBehavior measureView;
	[SerializeField] BeatInfoViewBehavior beatInfoView;
	[SerializeField] MeasureInfoViewBehavior measureInfoView;
	[SerializeField] MeasureAudioViewBehavior measureAudioView;

	BeatMapBlueprint blueprint;
	BlueprintDesigner designer;
	MeasureAudioClipManager clipManager;
	SongInfo songInfo;

	public event System.Action OnBack;

	void OnEnable () {
		backButton.onClick.AddListener ( () => { if (OnBack != null) OnBack (); } );
		saveButton.onClick.AddListener ( () => SongInfoIO.SaveInfo (songInfo) );
	}

	public void Wire (string trackName) {
		songInfo = SongInfoIO.LoadInfo (trackName);
		if (songInfo.blueprints.Count == 0)
			songInfo.blueprints.Add (new BeatMapBlueprint ());
		blueprint = songInfo.blueprints[0];

		designer = new BlueprintDesigner ();
		clipManager = new MeasureAudioClipManager (songInfo.bpm, songInfo.songDuration);

		measureView.Wire (designer, clipManager);
		beatInfoView.Wire (designer);
		measureInfoView.Wire (designer, blueprint);
		measureAudioView.Wire (designer, clipManager, trackName);

		designer.LoadBlueprint (blueprint);
	}
}
