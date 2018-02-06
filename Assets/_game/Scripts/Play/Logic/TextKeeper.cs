using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextKeeper {

	public event System.Action OnFinishText = delegate {}; 

	string text;
	int textIndex;

	public TextKeeper (string text) {
		this.text = text;
		textIndex = 0;
	}

	public char GetNextChar () {
		char nextChar = text[textIndex];
		textIndex++;
		if (textIndex >= text.Length)
			OnFinishText ();
		return nextChar;
	}
}