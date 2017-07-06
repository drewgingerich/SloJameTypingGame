using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	[SerializeField] InteractiveBeatSpawner interactiveBeatSpawner;
	[SerializeField] Passage passage;

	List<string> textLines;
	int textIndex;
	int charIndex;

	public event System.Action<int, string, string> OnChangeText;

	public char GetCurrentCharAtIndex(int relativeIndex) {
		return textLines [textIndex] [charIndex + relativeIndex];
	}

	void Awake () {
		textLines = new List<string> (passage.passageText.Split ('\n'));
		textIndex = 0;
		charIndex = 0;
	}

	void Start () {
		interactiveBeatSpawner.OnCreateInteractiveBeat += RegisterUpcomingBeat;
		OnChangeText (charIndex, textLines [textIndex], textLines [textIndex + 1]);
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
		OnChangeText (charIndex, textLines [textIndex], textLines [textIndex + 1]);
	}

	void RegisterUpcomingBeat (InteractiveBeat beat) {
		beat.OnLifetimeEnd += IncrementText;
	}
}
