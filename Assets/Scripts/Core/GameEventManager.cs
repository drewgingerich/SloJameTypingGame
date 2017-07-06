using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour {

	[SerializeField] SongTimeManager songTimeManager;

	public event System.Action<float> OnUpdateSongTime;
	public event System.Action<float> OnCheckForNewInteractiveBeats;
	public event System.Action OnHandleInput;


	void Start () {
		songTimeManager.OnReadPlayheadPosition += RunEvents;
	}

	void RunEvents (float songTimeDelta) {
		if (OnUpdateSongTime != null) {
			OnUpdateSongTime (songTimeDelta);
		}
		if (OnCheckForNewInteractiveBeats != null) {
			OnCheckForNewInteractiveBeats (songTimeDelta);
		}
		if (OnHandleInput != null) {
			OnHandleInput ();
		}
	}
}
