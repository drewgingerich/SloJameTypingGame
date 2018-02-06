using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawnerManager : MonoBehaviour {

	[SerializeField] IndicatorSpawner leftPinkySpawner;
	[SerializeField] IndicatorSpawner leftRingSpawner;
	[SerializeField] IndicatorSpawner leftMiddleSpawner;
	[SerializeField] IndicatorSpawner leftIndexSpawner;
	[SerializeField] IndicatorSpawner thumbSpawner;
	[SerializeField] IndicatorSpawner rightIndexSpawner;
	[SerializeField] IndicatorSpawner rightMiddleSpawner;
	[SerializeField] IndicatorSpawner rightRingSpawner;
	[SerializeField] IndicatorSpawner rightPinkySpawner;

	// Color mainIndicatorColor;
	// color supportindicatorcolor;

	// void awake () {
	// 	mainindicatorcolor = new color(1, 1, 1, 1f);
	// 	supportindicatorcolor = new color (0, 0, 1, 0f);
	// }

	public void Wire (BeatSpawner spawner) {
		spawner.OnSpawnBeat += DelegateSpawn;
	}

	void DelegateSpawn (Beat beat) {
		// SpawnAtMiddleSpawner (beat);
		SpawnByFinger (beat);
	}

	void SpawnAtMiddleSpawner (Beat beat) {
		thumbSpawner.SpawnIndicator (beat);
	}

	void SpawnByFinger (Beat beat) {
		char targetChar = beat.targetChar;
		switch (targetChar) {
			case '~':
			case '!':
			case 'Q':
			case 'A':
			case 'Z':
				// rightPinkySpawner.SpawnIndicator (beat);
				goto case 'z';
			case '`':
			case '1':
			case 'q':
			case 'a':
			case 'z':
				leftPinkySpawner.SpawnIndicator (beat);
				break;
			
			case '@':
			case 'W':
			case 'S':
			case 'X':
				// rightPinkySpawner.SpawnIndicator(beat);
				goto case 'x';
			case '2':
			case 'w':
			case 's':
			case 'x':
				leftRingSpawner.SpawnIndicator (beat);
				break;

			case '#':
			case 'E':
			case 'D':
			case 'C':
				// rightPinkySpawner.SpawnIndicator(beat);
				goto case 'c';
			case '3':
			case 'e':
			case 'd':
			case 'c':
				leftMiddleSpawner.SpawnIndicator (beat);
				break;

			case '$':
			case 'R':
			case 'F':
			case 'V':
			case '%':
			case 'T':
			case 'G':
			case 'B':
				// rightPinkySpawner.SpawnIndicator(beat);
				goto case 'b';
			case '4':
			case 'r':
			case 'f':
			case 'v':
			case '5':
			case 't':
			case 'g':
			case 'b':
				leftIndexSpawner.SpawnIndicator (beat);
				break;

			case ' ':
				thumbSpawner.SpawnIndicator (beat);
				break;

			case '^':
			case 'Y':
			case 'H':
			case 'N':
			case '&':
			case 'U':
			case 'J':
			case 'M':
				// leftPinkySpawner.SpawnIndicator(beat);
				goto case 'm';
			case '6':
			case 'y':
			case 'h':
			case 'n':
			case '7':
			case 'u':
			case 'j':
			case 'm':
				rightIndexSpawner.SpawnIndicator (beat);
				break;

			case '*':
			case 'I':
			case 'K':
			case '<':
				// leftPinkySpawner.SpawnIndicator(beat);
				goto case ',';
			case '8':
			case 'i':
			case 'k':
			case ',':
				rightMiddleSpawner.SpawnIndicator(beat);
				break;

			case '(':
			case 'O':
			case 'L':
			case '>':
				// leftPinkySpawner.SpawnIndicator(beat);
				goto case '.';
			case '9':
			case 'o':
			case 'l':
			case '.':
				rightRingSpawner.SpawnIndicator(beat);
				break;

			case ')':
			case 'P':
			case ':':
			case '?':
			case '_':
			case '{':
			case '"':
			case '+':
			case '}':
			case '|':
				// leftPinkySpawner.SpawnIndicator(beat);
				goto case '\n';
			case '0':
			case 'p':
			case ';':
			case '/':
			case '-':
			case '[':
			case '\'':
			case '=':
			case ']':
			case '\\':
			case '\n':
				rightPinkySpawner.SpawnIndicator(beat);
				break;

			default:
				Debug.LogError ("Character: " + beat.targetChar + " is not supported.");
				break;
		}
	}
}
