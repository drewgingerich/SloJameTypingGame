using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextViewBehavior : MonoBehaviour {

	[SerializeField] Text textComp;

	string text;

	public void Wire (TextManager textManager) {
		textManager.OnStartText += (text) => this.text = text;
		textManager.OnUpdateTextIndex += DisplayText;
	}

	void DisplayText (int currentCharIndex) {
		textComp.text = FormatText (currentCharIndex);
		PositionText ();
	}

	string FormatText(int currentCharIndex) {
		char currentChar = text [currentCharIndex];
		if (currentChar == ' ')
			currentChar = '_';

		StringBuilder stringBuilder = new StringBuilder ();
		stringBuilder.Append (System.String.Format ("<b><color=orange>{0}</color></b>", currentChar));
		stringBuilder.Append (text.Substring (currentCharIndex + 1));
		return stringBuilder.ToString ();
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

	// void FormatText(string currentLine, int charIndex, string nextLine) {
	// 	StringBuilder stringBuilder = new StringBuilder ();
	// 	stringBuilder.Append (currentLine);
	// 	stringBuilder.Append ('\n');

	// 	if (!System.String.IsNullOrEmpty(nextLine)) {
	// 		stringBuilder.Append ("<color=grey>");
	// 		stringBuilder.Append (nextLine);
	// 		stringBuilder.Append ("</color>");
	// 	}

	// 	char currentChar = currentLine [charIndex];
	// 	if (currentChar == ' ') {
	// 		currentChar = '_';
	// 	}

	// 	string highlightChar = System.String.Format ("<b><color=orange>{0}</color></b>", currentChar);
	// 	stringBuilder.Remove(charIndex, 1);
	// 	stringBuilder.Insert(charIndex, highlightChar);
	// 	textComp.text = stringBuilder.ToString();
	// }

	// void PositionText (int charIndex) {
	// 	TextGenerator textGen = new TextGenerator (textComp.text.Length);
	// 	Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
	// 	textGen.Populate (textComp.text, textComp.GetGenerationSettings (extents));

	// 	int indexOfTextQuad = charIndex * 4;
	// 	if (indexOfTextQuad < textGen.vertexCount) {
	// 		Vector3 avgPos = (
	// 			textGen.verts[indexOfTextQuad].position + 
	// 			textGen.verts[indexOfTextQuad + 1].position + 
	// 			textGen.verts[indexOfTextQuad + 2].position + 
	// 			textGen.verts[indexOfTextQuad + 3].position
	// 		) / 4f;
	// 		RectTransform rectTrans = textComp.rectTransform;
	// 		rectTrans.localPosition = new Vector3 (
	// 			avgPos.x * -1 - 7, rectTrans.localPosition.y , rectTrans.localPosition.z
	// 		);
	// 	}
	// 	else {
	// 		Debug.LogError ("Out of text bound");
	// 	}
	// }