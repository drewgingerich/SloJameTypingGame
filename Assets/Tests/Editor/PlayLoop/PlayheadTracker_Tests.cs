//using UnityEngine;
//using UnityEditor;
//using UnityEngine.TestTools;
//using NUnit.Framework;
//using System.Collections;
//
//public class PlayheadTracker_Tests {
//
//	[Test] 
//	public void TrackPlayheadPosition_FreshReportedPosition_PositionEqualsReportedValue () {
//		PlayheadTracker playheadTracker = new PlayheadTracker ();
//		float timeChange = 1f;
//		float freshPosition = 300f;
//
//		PlayheadPositionData data = playheadTracker.TrackPlayheadPosition (freshPosition, timeChange);
//
//		Assert.That (data.playheadPosition == freshPosition);
//	}
//
//	[Test] 
//	public void TrackPlayheadPosition_FreshReportedPosition_ShiftEqualsNewMinusOldPosition () {
//		PlayheadTracker playheadTracker = new PlayheadTracker ();
//		float startingPosition = 0f;
//		float timeChange = 1f;
//		float freshPosition = 300f;
//
//		PlayheadPositionData data = playheadTracker.TrackPlayheadPosition (freshPosition, timeChange);
//
//		Assert.That (data.playheadShift == freshPosition - startingPosition);
//	}
//
//	[Test] 
//	public void TrackPlayheadPosition_StaleReportedPosition_PositionIncreasesByTimeChange () {
//		PlayheadTracker playheadTracker = new PlayheadTracker ();
//		float timeChange = 1f;
//		float stalePosition = 0f;
//
//		PlayheadPositionData data = playheadTracker.TrackPlayheadPosition (stalePosition, timeChange);
//
//		Assert.That (data.playheadPosition == stalePosition + timeChange);
//	}
//
//	[Test] 
//	public void TrackPlayheadPosition_StaleReportedPosition_ShiftEqualsTimeChange () {
//		PlayheadTracker playheadTracker = new PlayheadTracker ();
//		float timeChange = 1f;
//		float stalePosition = 0f;
//
//		PlayheadPositionData data = playheadTracker.TrackPlayheadPosition (stalePosition, timeChange);
//
//		Assert.That (data.playheadShift == timeChange);
//	}
//
//}

//[Test]
//public void PlayheadPositionUpdate_StaleReportedPlayheadPosition_PositionAddsTimeChange() {
//	PlayheadTracker playheadTracker = new PlayheadTracker ();
//	float timeChange = 1f;
//	float stalePosition = 0f;
//
//	PlayheadPositionUpdate update = playheadTracker.GetPlayheadPositionUpdate (timeChange, stalePosition);
//	//update = playheadTracker.GetPlayheadPositionUpdate (timeChange, stalePosition);
//
//	Assert.That (update.newPlayheadPosition == timeChange * 2);
//}
//
//[Test]
//public void PlayheadPositionUpdate_StaleReportedPlayheadPosition_PositionChangeEqualsTimeChange() {
//	// Use the Assert class to test conditions.
//}
//
//[Test]
//public void PlayheadPositionUpdate_FreshReportedPlayheadPosition_PositionEqualsReportedValue() {
//	// Use the Assert class to test conditions.
//}
//
//[Test]
//public void PlayheadPositionUpdate_FreshReportedPlayheadPosition_PositionChangeEqualsReportedMinusOldValue() {
//	// Use the Assert class to test conditions.
//}
