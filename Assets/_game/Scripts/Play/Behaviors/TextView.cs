using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextView : MonoBehaviour {

	[SerializeField] Text textComp;

	string text;
	int textIndex;

	public void Wire (string text, ScoringChecker scoringChecker, BeatActivityMonitor activityMonitor) {
		this.text = text;
		scoringChecker.OnScoreBeat += UpdateText;
		activityMonitor.OnMissedBeat += UpdateText;
		textIndex = 0;
		DisplayText (0);
	}

	void UpdateText () {
		textIndex++;
		DisplayText (textIndex);
	}

	void DisplayText (int textIndex) {
		textComp.text = FormatText (textIndex);
		PositionText ();
	}

	string FormatText(int textIndex) {
		if (textIndex >= text.Length)
			return "";
		string newText = text.Substring(textIndex);
		newText = newText.Replace(' ', '\u02FD').Replace("\n", "↲\n");
		return string.Format ("<b><color=orange>{0}</color></b>{1}", newText[0], newText.Substring(1));
	}

	void PositionText () {
		TextGenerator textGen = new TextGenerator (textComp.text.Length);
		Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
		textGen.Populate (textComp.text, textComp.GetGenerationSettings (extents));

		int indexOfTextQuad = 0;
		if (indexOfTextQuad < textGen.vertexCount) {
			Vector3 avgPos = (
				textGen.verts[indexOfTextQuad].position + 
				textGen.verts[indexOfTextQuad + 1].position + 
				textGen.verts[indexOfTextQuad + 2].position + 
				textGen.verts[indexOfTextQuad + 3].position
			) / 4f;
			RectTransform rectTrans = textComp.rectTransform;
			rectTrans.localPosition = new Vector3 (
				avgPos.x * -1 - 5, rectTrans.localPosition.y , rectTrans.localPosition.z
			);
		}
		else {
			Debug.LogError ("Out of text bound");
		}
	}
}