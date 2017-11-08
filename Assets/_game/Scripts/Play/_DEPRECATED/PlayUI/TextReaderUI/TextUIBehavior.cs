// using System.Text;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class TextUIBehavior : MonoBehaviour {

// 	[SerializeField] Text textComp;
// 	[SerializeField] TapMonitorBehavior beatSystem;

// 	TextManager passageReader;

// 	void Start () {
// 		passageReader = beatSystem.PassageReader;
// 		passageReader.OnReadNewCharacter += DisplayText;
// 	}

// 	public void DisplayText (int charIndex, string currentLine, string nextLine) {
// 		SetTextColors (currentLine, charIndex, nextLine);
// 		SetTextPositions (charIndex);
// 	}

// 	void SetTextColors(string currentLine, int charIndex, string nextLine) {
// 		StringBuilder stringBuilder = new StringBuilder ();
// 		stringBuilder.Append (currentLine);
// 		stringBuilder.Append ('\n');

// 		if (!System.String.IsNullOrEmpty(nextLine)) {
// 			stringBuilder.Append ("<color=grey>");
// 			stringBuilder.Append (nextLine);
// 			stringBuilder.Append ("</color>");
// 		}

// 		char currentChar = currentLine [charIndex];
// 		if (currentChar == ' ') {
// 			currentChar = '_';
// 		}

// 		string highlightChar = System.String.Format ("<b><color=orange>{0}</color></b>", currentChar);
// 		stringBuilder.Remove(charIndex, 1);
// 		stringBuilder.Insert(charIndex, highlightChar);
// 		textComp.text = stringBuilder.ToString();
// 	}

// 	void SetTextPositions (int charIndex) {
// 		TextGenerator textGen = new TextGenerator (textComp.text.Length);
// 		Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
// 		textGen.Populate (textComp.text, textComp.GetGenerationSettings (extents));

// 		int indexOfTextQuad = charIndex * 4;
// 		if (indexOfTextQuad < textGen.vertexCount) {
// 			Vector3 avgPos = (
// 				textGen.verts[indexOfTextQuad].position + 
// 				textGen.verts[indexOfTextQuad + 1].position + 
// 				textGen.verts[indexOfTextQuad + 2].position + 
// 				textGen.verts[indexOfTextQuad + 3].position
// 			) / 4f;
// 			RectTransform rectTrans = textComp.rectTransform;
// 			rectTrans.localPosition = new Vector3 (
// 				avgPos.x * -1 - 7, rectTrans.localPosition.y , rectTrans.localPosition.z
// 			);
// 		}
// 		else {
// 			Debug.LogError ("Out of text bound");
// 		}
// 	}
// }
