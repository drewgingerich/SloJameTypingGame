// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class TrimMusicUIBehavior : MonoBehaviour {

// 	[SerializeField] Button backButton;
// 	[SerializeField] Button playButton;
// 	[SerializeField] AudioSource audioSource;
// 	[SerializeField] InputField startTimeInput;
// 	[SerializeField] InputField endTimeInput;

// 	TrackPlayInfo parameters;
	
// 	public event System.Action OnBack;

// 	void Start () {
// 		backButton.onClick.AddListener( () => { if (OnBack != null) OnBack (); } );
// 		playButton.onClick.AddListener( () => { PlayClip (); } );
// 		startTimeInput.onEndEdit.AddListener( delegate { UpdateStartTime (startTimeInput.text); } );
// 		endTimeInput.onEndEdit.AddListener( delegate { UpdateEndTime (endTimeInput.text); } );
// 	}

// 	void OnEnable () {
// 		playButton.interactable = false;
// 	}

// 	void OnDisable () {
// 		if (audioSource.isPlaying)
// 			audioSource.Stop ();
// 		// audioSource.clip = null;
// 	}

// 	void Update () {
// 		if (audioSource.time >= parameters.endTime && audioSource.isPlaying)
// 			audioSource.Stop ();
// 	}

// 	public void LoadMusicTrack (string musicTrackName) {
// 		parameters = TrackPlayInfoIO.LoadFromFile (musicTrackName);
// 		audioSource.clip = AudioClip.Create (
// 			parameters.trackName, parameters.samples, parameters.channels, parameters.frequency, false
// 		);
// 		audioSource.clip.SetData (parameters.data, 0);
// 		playButton.interactable = true;
// 	}

// 	void PlayClip () {
// 		audioSource.time = parameters.startTime;
// 		audioSource.Play ();
// 	}

// 	void UpdateStartTime (string newStartTime) {
// 		try {
// 			parameters.startTime = float.Parse(newStartTime);
// 			TrackPlayInfoIO.SaveToFile (parameters);
// 		} catch {}
// 		startTimeInput.text = parameters.startTime.ToString ();
// 	}

// 	void UpdateEndTime (string newEndTime) {
// 		try {
// 			parameters.endTime = float.Parse(newEndTime);
// 			TrackPlayInfoIO.SaveToFile (parameters);
// 		} catch {}
// 		endTimeInput.text = parameters.endTime.ToString ();
// 	}
// }
