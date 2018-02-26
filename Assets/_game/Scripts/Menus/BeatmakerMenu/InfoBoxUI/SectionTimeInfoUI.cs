using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionTimeInfoUI : MonoBehaviour {

	[SerializeField] Text timeSectionText;

	float startTime;
	float endTime;

	public void UpdateTimeSection (float newStartTime, float newEndTime) {
		startTime = newStartTime;
		endTime = newEndTime;
		DisplayTimeSection();
	}

	void DisplayTimeSection() {
		timeSectionText.text = string.Format("{0:0.00} - {1:0.00} sec", startTime, endTime);
	}
}
