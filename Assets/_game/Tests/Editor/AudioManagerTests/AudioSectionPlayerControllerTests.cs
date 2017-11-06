using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AudioSectionPlayerControllerTests {

	AudioSectionPlayerController controller;
	float someClipLength = 5;
	float someEndTime = 10;
	float someTimeChange = 2;

	[SetUp]
	public void SetUp () {
		PlayheadTracker tracker = new PlayheadTracker ();
		AudioSectionStateManager stateManager = new AudioSectionStateManager ();
		controller = new AudioSectionPlayerController (tracker, stateManager);
		controller.SetClipLength (someClipLength);
	}

	[Test]
	public void PlayAudioSection_EmitsOnStartSectionEvent () {
		float startTime = 1;
		bool eventEmitted = false;
		controller.OnStartSection += () => eventEmitted = true;

		controller.PlayAudioSection (startTime, someEndTime);

		Assert.True (eventEmitted);
	}

	[Test]
	public void MonitorSectionProgress_PositionStartsBelowZero_UsesIndirectTracking () {
		float startTime = -5;
		controller.PlayAudioSection (startTime, someEndTime);
		float freshPositionReport = startTime + 1;
		float capturedPosition = float.NegativeInfinity;
		controller.OnUpdatePosition += (position) => capturedPosition = position;

		controller.MonitorSectionProgress (freshPositionReport, someTimeChange);

		Assert.That (capturedPosition == startTime + someTimeChange);
	}

	[Test]
	public void MonitorSectionProgress_PositionStartsWithinClipLength_UsesDirectTracking () {
		float startTime = 1;
		controller.PlayAudioSection (startTime, someEndTime);
		float freshPositionReport = startTime + 1;
		float capturedPosition = float.NegativeInfinity;
		controller.OnUpdatePosition += (position) => capturedPosition = position;

		controller.MonitorSectionProgress (freshPositionReport, someTimeChange);

		Assert.That (capturedPosition == freshPositionReport);
	}

	[Test]
	public void MonitorSectionProgress_PositionStartsAfterClipLength_UsesDirectTracking () {
		float startTime = 1;
		controller.PlayAudioSection (startTime, someEndTime);
		float freshPositionReport = startTime + 1;
		float capturedPosition = float.NegativeInfinity;
		controller.OnUpdatePosition += (position) => capturedPosition = position;

		controller.MonitorSectionProgress (freshPositionReport, someTimeChange);

		Assert.That (capturedPosition == freshPositionReport);
	}

	[Test]
	public void MonitorSectionProgress_PositionMovesPastZero_EmitsOnAudioStartWithNewPosition () {
		float startTime = -1;
		controller.PlayAudioSection (startTime, someEndTime);
		float capturedPosition = float.NegativeInfinity;
		controller.OnStartAudio += (position) => capturedPosition = position;

		controller.MonitorSectionProgress (startTime, someTimeChange);

		Assert.That (capturedPosition == startTime + someTimeChange);
	}

	[Test]
	public void MonitorSectionProgress_PositionMovesPastClipLength_EmitsOnAudioEnd () {
		float startTime = 1;
		controller.PlayAudioSection (startTime, someEndTime);
		float positionReport = 8;
		bool eventEmitted = false;
		controller.OnEndAudio += () => eventEmitted = true;

		controller.MonitorSectionProgress (positionReport, someTimeChange);

		Assert.True (eventEmitted);
	}

	[Test]
	public void MonitorSectionProgress_Paused_NoTracking () {
		float startTime = 1;
		controller.PlayAudioSection (startTime, someEndTime);
		controller.paused = true;
		float somePositionReport = 8;
		bool eventEmitted = false;
		controller.OnUpdatePosition += (_) => eventEmitted = true;

		controller.MonitorSectionProgress (somePositionReport, someTimeChange);

		Assert.False (eventEmitted);
	}

	[Test]
	public void MonitorSectionProgress_PositionMovesPastEndTime_EmitsOnEndSectionEvent () {
		float startTime = 1;
		controller.PlayAudioSection (startTime, someEndTime);
		float someLargePositionReport = someEndTime + 1;
		bool eventEmitted = false;
		controller.OnEndSection += () => eventEmitted = true;

		controller.MonitorSectionProgress (someLargePositionReport, someTimeChange);

		Assert.True (eventEmitted);
	}
}
