using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AudioSectionStateManagerTests {

	AudioSectionStateManager manager;
	float someAudioEndTime = 10;

	[SetUp]
	public void SetUp () {
		manager = new AudioSectionStateManager ();
		manager.SetAudioEndTime (someAudioEndTime);
	}

	[Test]
	public void InitializeStateManagment_PositionWithinAudio_EmitsOnStartAudioEventWithPosition () {
		float emittedPosition = float.NegativeInfinity;
		manager.OnStartAudio += (position) => emittedPosition = position;

		manager.InitializeStateManagment (1);

		Assert.That (emittedPosition == 1);
	}

	[Test]
	public void UpdateState_PositionPassesZero_EmitsOnStartAudioEvent () {
		manager.InitializeStateManagment (-1);
		bool eventEmitted = false;
		manager.OnStartAudio += (position) => eventEmitted = true;

		manager.UpdateState (2);

		Assert.True (eventEmitted);
	}

	[Test]
	public void UpdateState_PositionPassesAudioEndTime_EmitsOnEndAudioEvent () {
		manager.InitializeStateManagment (0);
		bool eventEmitted = false;
		manager.OnEndAudio += () => eventEmitted = true;

		manager.UpdateState (someAudioEndTime + 1);

		Assert.True (eventEmitted);
	}

	[Test]
	public void InAudioState_InitializedWithPositionBeforeAudio_ReturnsFalse() {
		manager.InitializeStateManagment (-1);

		Assert.False (manager.InAudioState ());
	}

	[Test]
	public void InAudioState_InitializedWithPositionWithinAudio_ReturnsTrue() {
		manager.InitializeStateManagment (someAudioEndTime - 1);

		Assert.True(manager.InAudioState ());
	}

	[Test]
	public void InAudioState_InitializedWithPositionAfterAudio_ReturnsFalse() {
		manager.InitializeStateManagment (someAudioEndTime + 1);

		Assert.False (manager.InAudioState ());
	}
}
