using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	[SerializeField] Passage passage;
	[SerializeField] InteractiveBeatSpawner interactiveBeatSpawner;

	List<string> textLines;
	int textIndex;
	int charIndex;

	public event System.Action<int, string, string> OnChangeText;

	public char GetUpcomingCharacter(int upcomingCharIndex) {
		return textLines [textIndex] [charIndex + upcomingCharIndex];
	}

	void Awake () {
		textLines = new List<string> (passage.passageText.Split ('\n'));
		textIndex = 0;
		charIndex = 0;
	}

	void Start () {
		OnChangeText (charIndex, textLines [textIndex], textLines [textIndex + 1]);
		interactiveBeatSpawner.OnSpawnInteractiveBeat += RegisterInteractiveBeat;
	}
		
	void IncrementText() {
		Debug.Log ("OnIncremetText");
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

	void RegisterInteractiveBeat (InteractiveBeat interactiveBeat) {
		interactiveBeat.OnDestroy += IncrementText;
	}
}
