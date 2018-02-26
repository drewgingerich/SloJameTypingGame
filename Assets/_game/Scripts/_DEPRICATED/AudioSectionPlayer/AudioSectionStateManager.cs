// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AudioSectionStateManager {

// 	public event System.Action<float> OnStartAudio;
// 	public event System.Action OnEndAudio;

// 	enum SectionAudioState {PreAudio, Audio, PostAudio}
	
// 	SectionAudioState audioState;
// 	float audioEndTime;

// 	public void SetAudioEndTime (float audioEndTime) {
// 		this.audioEndTime = audioEndTime;
// 	}

// 	public void InitializeStateManagment (float sectionStartTime) {
// 		if (sectionStartTime < 0)
// 			audioState = SectionAudioState.PreAudio;
// 		else if (sectionStartTime > audioEndTime)
// 			audioState = SectionAudioState.PostAudio;
// 		else {
// 			audioState = SectionAudioState.Audio;
// 			if (OnStartAudio != null) OnStartAudio (sectionStartTime);
// 		}
// 	}

// 	public void UpdateState (float newPlayheadPosition) {
// 		if (audioState == SectionAudioState.PreAudio && newPlayheadPosition >= 0) {
// 			audioState = SectionAudioState.Audio;
// 			if (OnStartAudio != null) OnStartAudio (newPlayheadPosition);
// 		} else if (audioState == SectionAudioState.Audio && newPlayheadPosition >= audioEndTime) {
// 			audioState = SectionAudioState.PostAudio;
// 			if (OnEndAudio != null) OnEndAudio ();
// 		}
// 	}

// 	public bool InAudioState () {
// 		return audioState == SectionAudioState.Audio ? true : false;
// 	}
// }
