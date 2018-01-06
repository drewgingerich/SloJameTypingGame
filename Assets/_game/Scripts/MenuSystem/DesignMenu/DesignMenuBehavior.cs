using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignMenuBehavior : MonoBehaviour {

	public event System.Action OnBack = delegate {};

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

	void Awake () {
		designer = new DesignMenuController ();
		backButton.onClick.AddListener (() => OnBack());
		saveButton.onClick.AddListener (() => songData.Save());
	}

	public void Load (SongData songData, BeatmapBlueprint blueprint) {
		gameObject.SetActive (true);

		this.songData = songData;
		
		audioSectioner = new MeasureAudioController (songData.bpm);

		measureView.Load (designer, audioSectioner);
		beatInfoView.Load (designer);
		measureInfoView.Load (designer, blueprint);
		measureAudioView.Load (designer, audioSectioner, songData);

		designer.LoadBlueprint (blueprint);
	}

	public void Unload () {
		gameObject.SetActive (false);
	}
}
