using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager {

	public event System.Action<string> OnStartText = delegate {};
	public event System.Action OnEndText = delegate {};
	public event System.Action<int> OnUpdateTextIndex = delegate {};

	string text;
	public int textIndex { get; private set; }

	public TextManager (BeatActivityMonitor beatActivityMonitor) {
		beatActivityMonitor.OnMissedBeat += (beat) => UpdateTextIndex (beat.textIndex);
	}

	public void LoadText (string text) {
		this.text = text;
		textIndex = 0;
		OnStartText (text);
		OnUpdateTextIndex (textIndex);
	}

	public char GetCharacterAtIndex (int index) {
		return text[index];
	}

	public void UpdateTextIndex (int newIndex) {
		textIndex = newIndex;
		if (textIndex >= text.Length) {
			OnEndText ();
			return;
		}
		OnUpdateTextIndex (textIndex);
	}
}
