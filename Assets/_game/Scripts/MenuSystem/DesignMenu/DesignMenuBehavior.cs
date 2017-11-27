using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignMenuBehavior : MonoBehaviour {

	[SerializeField] Button backButton;
	[SerializeField] Button saveButton;
	[SerializeField] MeasureViewBehavior measureView;
	[SerializeField] BeatInfoViewBehavior beatInfoView;
	[SerializeField] MeasureInfoViewBehavior measureInfoView;
	[SerializeField] MeasureAudioBehavior measureAudioView;

	BeatmapBlueprint blueprint;
	DesignMenuController designer;
	MeasureAudioController audioSectioner;
	SongData songData;

	public event System.Action OnBack;

	void OnEnable () {
		backButton.onClick.AddListener ( () => { if (OnBack != null) OnBack (); } );
		saveButton.onClick.AddListener (songData.Save);
	}

	public void Wire (SongData songData) {
		this.songData = songData;
		if (songData.blueprints.Count == 0)
			songData.blueprints.Add (new BeatmapBlueprint ());
		blueprint = songData.blueprints[0];

		designer = new DesignMenuController ();
		audioSectioner = new MeasureAudioController (songData.bpm);

		measureView.Wire (designer, audioSectioner);
		beatInfoView.Wire (designer);
		measureInfoView.Wire (designer, blueprint);
		measureAudioView.Wire (designer, audioSectioner, songData.songTitle);

		designer.LoadBlueprint (blueprint);
	}
}
