using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockSessionStarter : MonoBehaviour {

	[SerializeField] PlayManagerBehavior managerBehavior;
	[SerializeField] AudioClip clip;
	[SerializeField] [TextArea] string text;
	[SerializeField] List<float> beatTimes;

	// Use this for initialization
	void Start () {
		BeatMap newMap = new BeatMap (beatTimes);
		managerBehavior.StartPlay (clip, newMap, text);
	}
}
