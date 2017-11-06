using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayheadTrackerTests {

	PlayheadTracker tracker;
	float startingPosition = 1f;

	[SetUp]
	public void SetUp () {
		tracker = new PlayheadTracker ();
		tracker.InitializeTracking (startingPosition);
	}

	[Test]
	public void TrackByReportedPosition_StaleReport_PositionIncreasesByTimeChange() {
		float staleReport = startingPosition;
		float timeChange = 2f;
		tracker.OnUpdatePosition += (position) => Assert.That (position == startingPosition + timeChange);

		tracker.TrackByReportedPosition (staleReport, timeChange);
	}

	[Test]
	public void TrackByReportedPosition_FreshReport_UpdatesPositionToFreshReport() {
		float freshReport = startingPosition + 1;
		float timeChange = 2f;
		tracker.OnUpdatePosition += (position) => Assert.That (position == freshReport);

		tracker.TrackByReportedPosition (freshReport, timeChange);
	}

	[Test]
	public void TrackByTimeChange_PositiveTimeChange_PositionIncreasesByTimeChange() {
		float timeChange = 2f;
		tracker.OnUpdatePosition += (position) => Assert.That (position == startingPosition + timeChange);

		tracker.TrackByTimeChange (timeChange);
	}
}
