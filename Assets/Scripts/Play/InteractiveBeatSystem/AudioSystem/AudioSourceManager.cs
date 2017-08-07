//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class AudioSourceManager : MonoBehaviour {
//
//	[SerializeField] private AudioSource audioSource;
//	private PlayheadTracker playheadTracker;
//
//	public PlayheadPositionData TrackPlayheadPosition () {
//		return playheadTracker.TrackPlayheadPosition (audioSource.time, Time.deltaTime);
//	}
//
//	public void StartAudio () {
//		audioSource.Play ();
//	}
//
//	public void StopAudio () {
//		audioSource.Stop ();
//	}
//
//	public void PauseAudio () {
//		audioSource.Pause ();
//	}
//
//	public void UnPauseAudio () {
//		audioSource.UnPause ();
//	}
//}
