using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	TextUI textUI;

	[SerializeField] Passage passage;

	List<string> textLines;
	int textIndex;
	int charIndex;

	void Awake () {
		textUI = gameObject.GetComponent<TextUI> ();
		textLines = new List<string> (passage.passageText.Split ('\n'));
		textIndex = 0;
		charIndex = 0;
	}

	void Start () {
		textUI.DisplayText (textLines [0], 0, textLines [1]);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			IncrementText ();
		}
	}

	bool CheckIfCorrectCharacter(char character) {
		return character == textLines[textIndex][charIndex];
	}

	void IncrementText() {
		charIndex = (charIndex + 1) % textLines[textIndex].Length;
		if (charIndex == 0) {
			textIndex = (textIndex + 1) % textLines.Count;
		}
		string currentLine = textLines [textIndex];
		string nextLine = null;
		if (textIndex < textLines.Count - 1) {
			nextLine = textLines [textIndex + 1];
		}
		textUI.DisplayText (textLines [textIndex], charIndex, nextLine);
	}
		
	void RoundFinish() {
		Debug.Log ("Round finished!!! Good Job!!!");
	}
}
