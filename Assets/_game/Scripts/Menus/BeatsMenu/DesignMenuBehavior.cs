using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignMenuBehavior : SnapMenu {

	[SerializeField] MeasureViewBehavior measureView;
	[SerializeField] BeatInfoViewBehavior beatInfoView;
	[SerializeField] MeasureInfoViewBehavior measureInfoView;
	[SerializeField] MeasureAudioBehavior measureAudioView;

	public void LoadNext(GameObject next) {
		Unload();
		next.SetActive(true);
	}

	public void SaveChanges() {
		DataNavigator.currentSong.Save();
	}

	void OnEnable() {
		DesignMenuController designer = new DesignMenuController();

		SongData songData = DataNavigator.GetCurrentSongData();
		BeatmapBlueprint blueprint = DataNavigator.GetCurrentBlueprint();

		MeasureAudioController audioSectioner = new MeasureAudioController(songData.bpm);

		measureView.Load(designer, audioSectioner);
		beatInfoView.Load(designer);
		measureInfoView.Load(designer, blueprint);
		measureAudioView.Load(designer, audioSectioner, songData);

		designer.LoadBlueprint(blueprint);
	}

	void Unload() {
		gameObject.SetActive(false);
	}
}
