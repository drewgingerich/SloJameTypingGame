// using UnityEngine;
// using UnityEditor;
// using UnityEngine.TestTools;
// using NUnit.Framework;
// using System.Collections;

// public class MeasureAudioClipManangerTests {

// 	public MeasureAudioController controller;

// 	public float someSongDuration = 120f;
// 	public float someBPM = 60f;
// 	public bool[] someMeasure = new bool[0];

// 	public float measureTime;

// 	[OneTimeSetUp]
// 	public void OneTimeSetup () {
// 		measureTime = 4 * 60 / someBPM;
// 	}

// 	[SetUp]
// 	public void SetUp () {
// 		controller = new MeasureAudioController (someBPM);
// 	}

// 	[Test]
// 	public void SetClipBounds_FirstMeasure_UpperBoundEqualsEndOfMeasureTimePlusQuarterMeasure() {
// 		int firstMeasureIndex = 0;
// 		controller.OnFindSectionBounds += (_, upper) => Assert.That (upper == measureTime * 1.25);

// 		controller.FindSectionForMeasure (firstMeasureIndex, someMeasure);
// 	}

// 	[Test]
// 	public void SetClipBounds_SecondMeasure_LowerBoundEqualsStartOfMeasureMinusQuarterMeasure() {
// 		int secondMeasureIndex = 1;
// 		controller.OnFindSectionBounds += (lower, _) => Assert.That (lower == measureTime * 0.75);

// 		controller.FindSectionForMeasure (secondMeasureIndex, someMeasure);
// 	}

// 	[Test]
// 	public void SetClipBounds_VeryLargeMeasure_LowerBoundEqualsSongDuration() {
// 		int someVeryLargeMeasureIndex = 10000;
// 		controller.OnFindSectionBounds += (lower, _) => Assert.That (lower == someSongDuration);

// 		controller.FindSectionForMeasure (someVeryLargeMeasureIndex, someMeasure);
// 	}

// 	[Test]
// 	public void SetClipBounds_VeryLargeMeasure_UpperBoundEqualsSongDuration() {
// 		int someVeryLargeMeasureIndex = 10000;
// 		controller.OnFindSectionBounds += (_, upper) => Assert.That (upper == someSongDuration);

// 		controller.FindSectionForMeasure (someVeryLargeMeasureIndex, someMeasure);
// 	}

// 	[Test]
// 	public void MonitorClipProgress_ClipNotOver_NoEventEmitted() {
// 		float someTimeSmallerThanSongDuration = someSongDuration - 1;
// 		bool eventCalled = false;
// 		controller.OnEndSection += () => eventCalled = true;

// 		controller.MonitorSectionProgress (someTimeSmallerThanSongDuration);
// 		Assert.That (eventCalled == true);
// 	}

// 	[Test]
// 	public void MonitorClipProgress_ClipOver_NoEventEmitted() {
// 		float someTimeGreaterThanSongDuration = someSongDuration + 1;
// 		bool eventCalled = false;
// 		controller.OnEndSection += () => eventCalled = true;

// 		controller.MonitorSectionProgress (someTimeGreaterThanSongDuration);
// 		Assert.That (eventCalled == true);
// 	}
// }
