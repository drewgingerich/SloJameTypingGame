using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MeasureAudioClipManangerTests {

	public MeasureAudioClipManager controller;

	public float someSongDuration = 120f;
	public float someBPM = 60f;
	public bool[] someMeasure = new bool[0];

	public float measureTime;

	[OneTimeSetUp]
	public void OneTimeSetup () {
		measureTime = 4 * 60 / someBPM;
	}

	[SetUp]
	public void SetUp () {
		controller = new MeasureAudioClipManager (someBPM, someSongDuration);
	}

	[Test]
	public void SetClipBounds_FirstMeasure_UpperBoundEqualsEndOfMeasureTimePlusQuarterMeasure() {
		int firstMeasureIndex = 0;
		controller.OnSetClipBounds += (_, upper) => Assert.That (upper == measureTime * 1.25);

		controller.SetClipBounds (firstMeasureIndex, someMeasure);
	}

	[Test]
	public void SetClipBounds_SecondMeasure_LowerBoundEqualsStartOfMeasureMinusQuarterMeasure() {
		int secondMeasureIndex = 1;
		controller.OnSetClipBounds += (lower, _) => Assert.That (lower == measureTime * 0.75);

		controller.SetClipBounds (secondMeasureIndex, someMeasure);
	}

	[Test]
	public void SetClipBounds_VeryLargeMeasure_LowerBoundEqualsSongDuration() {
		int someVeryLargeMeasureIndex = 10000;
		controller.OnSetClipBounds += (lower, _) => Assert.That (lower == someSongDuration);

		controller.SetClipBounds (someVeryLargeMeasureIndex, someMeasure);
	}

	[Test]
	public void SetClipBounds_VeryLargeMeasure_UpperBoundEqualsSongDuration() {
		int someVeryLargeMeasureIndex = 10000;
		controller.OnSetClipBounds += (_, upper) => Assert.That (upper == someSongDuration);

		controller.SetClipBounds (someVeryLargeMeasureIndex, someMeasure);
	}

	[Test]
	public void MonitorClipProgress_ClipNotOver_NoEventEmitted() {
		float someTimeSmallerThanSongDuration = someSongDuration - 1;
		bool eventCalled = false;
		controller.OnStopAudio += () => eventCalled = true;

		controller.MonitorClipProgress (someTimeSmallerThanSongDuration);
		Assert.That (eventCalled == true);
	}

	[Test]
	public void MonitorClipProgress_ClipOver_NoEventEmitted() {
		float someTimeGreaterThanSongDuration = someSongDuration + 1;
		bool eventCalled = false;
		controller.OnStopAudio += () => eventCalled = true;

		controller.MonitorClipProgress (someTimeGreaterThanSongDuration);
		Assert.That (eventCalled == true);
	}
}
