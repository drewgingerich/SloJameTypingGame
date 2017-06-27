using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour {

	Text textComp;

	void Awake() {
		textComp = gameObject.GetComponent<Text> ();
	}

	public void DisplayText (string currentLine, int charIndex, string nextLine) {
		SetTextColor (currentLine, charIndex, nextLine);
		SetTextPosition (charIndex);
	}

	void SetTextColor(string currentLine, int charIndex, string nextLine) {
		StringBuilder sBuilder = new StringBuilder ();
		sBuilder.Append (currentLine);
		sBuilder.Append ('\n');
		if (!System.String.IsNullOrEmpty(nextLine)) {
			sBuilder.Append ("<color=grey>");
			sBuilder.Append (nextLine);
			sBuilder.Append ("</color>");
		}
		char currentChar = currentLine [charIndex];
		if (currentChar == ' ') {
			currentChar = '_';
		}
		string highlightChar = System.String.Format ("<b><color=orange>{0}</color></b>", currentChar);
		sBuilder.Remove(charIndex, 1);
		sBuilder.Insert(charIndex, highlightChar);
		textComp.text = sBuilder.ToString();
	}

	void SetTextPosition (int charIndex) {
		TextGenerator textGen = new TextGenerator (textComp.text.Length);
		Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
		textGen.Populate (textComp.text, textComp.GetGenerationSettings (extents));
		int indexOfTextQuad = charIndex * 4;

		if (indexOfTextQuad < textGen.vertexCount)
		{
			Vector3 avgPos = (
				textGen.verts[indexOfTextQuad].position + 
				textGen.verts[indexOfTextQuad + 1].position + 
				textGen.verts[indexOfTextQuad + 2].position + 
				textGen.verts[indexOfTextQuad + 3].position
			) / 4f;
			Debug.Log (avgPos.x);
			RectTransform rectTrans = gameObject.GetComponent<RectTransform> ();
			rectTrans.localPosition = new Vector3 (
				avgPos.x * -1, rectTrans.localPosition.y , rectTrans.localPosition.z
			);
		}
		else {
			Debug.LogError ("Out of text bound");
		}
	}
}
