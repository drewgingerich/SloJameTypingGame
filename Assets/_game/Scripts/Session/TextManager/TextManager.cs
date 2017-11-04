using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager {

	public event System.Action OnTextEnd;

	string text;
	int textIndex;

	public TextManager (string text, BeatSpawner beatSpawner) {
		beatSpawner.OnSpawnBeat += RegisterBeat;
		this.text = text;
		textIndex = 0;
	}

	public char GetNextCharacter () {
		return text[textIndex];
	}

	void RegisterBeat (Beat beat) {
		beat.OnDestroy += DeregisterBeat;
	}

	void DeregisterBeat (Beat beat) {
		IncrementText ();
		beat.OnDestroy -= DeregisterBeat;
	}

	void IncrementText () {
		textIndex++;
		if (textIndex >= text.Length) {
			if (OnTextEnd != null)
				OnTextEnd ();
		}
	}
}
