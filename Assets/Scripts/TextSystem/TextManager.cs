using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	[SerializeField] BeatReader beatReader;
	[SerializeField] Passage passage;

	List<string> textLines;
	int textIndex;
	int charIndex;

	public event System.Action<int, string, string> OnTextChange;

	void Awake () {
		textLines = new List<string> (passage.passageText.Split ('\n'));
		textIndex = 0;
		charIndex = 0;
	}

	void Start () {
		beatReader.OnBeatEnd += IncrementText;
		OnTextChange (charIndex, textLines [textIndex], textLines [textIndex + 1]);
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
		OnTextChange (charIndex, textLines [textIndex], textLines [textIndex + 1]);
	}

	public char GetCurrentChar() {
		return textLines [textIndex] [charIndex];
	}
}
