using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager {

	public event System.Action<string> OnStartText;
	public event System.Action OnEndText;
	public event System.Action<int> OnUpdateTextIndex;

	string text;
	int textIndex;

	public void LoadText (string text) {
		this.text = text;
		textIndex = 0;
		if (OnStartText != null)
			OnStartText (text);
		if (OnUpdateTextIndex != null)
			OnUpdateTextIndex (textIndex);
	}

	public char GetNextCharacter () {
		return text[textIndex];
	}

	public void IncrementText () {
		textIndex++;
		if (textIndex >= text.Length) {
			if (OnEndText != null)
				OnEndText ();
			return;
		}
		if (OnUpdateTextIndex != null)
			OnUpdateTextIndex (textIndex);
	}
}
