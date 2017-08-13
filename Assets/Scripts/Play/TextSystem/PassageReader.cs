using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageReader {

	Passage passage;

	List<string> textLines;
	int textIndex;
	int charIndex;

	public event System.Action<int, string, string> OnReadNewCharacter;

	public PassageReader (Passage passage) {
		this.passage = passage;
		textLines = new List<string> (passage.passageText.Split ('\n'));
		textIndex = 0;
		charIndex = 0;
	}

	public char GetUpcomingCharacter (int upcomingCharIndex) {
		return textLines [textIndex] [charIndex + upcomingCharIndex];
	}
		
	public void IncrementText (int numberToIncrementBy) {
		charIndex = charIndex + numberToIncrementBy;
		int numCharsInCurrentLine = textLines [textIndex].Length;
		if (charIndex >= numCharsInCurrentLine) {
			charIndex %= numCharsInCurrentLine;
			textIndex = (textIndex + 1) % textLines.Count;
		}
		string currentLine = textLines [textIndex];
		string nextLine = null;
		if (textIndex < textLines.Count - 1)
			nextLine = textLines [textIndex + 1];
		if (OnReadNewCharacter != null)
			OnReadNewCharacter (charIndex, textLines [textIndex], textLines [(textIndex + 1) % textLines.Count]);
	}
}
