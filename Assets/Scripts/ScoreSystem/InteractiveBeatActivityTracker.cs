using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatActivityTracker : MonoBehaviour {

	[SerializeField] PlaySettings playSettings;
	[SerializeField] InteractiveBeatSpawner interactiveBeatSpawner;

	public List<InteractiveBeat> ActiveInteractiveBeats { get; private set; }

	public event System.Action<InteractiveBeat> OnActivateInteractiveBeat;
	public event System.Action<InteractiveBeat> OnDeactivateInteractiveBeat;

	void Awake () {
		ActiveInteractiveBeats = new List<InteractiveBeat> ();
	}

	void Start () {
		interactiveBeatSpawner.OnSpawnInteractiveBeat += RegisterInteractiveBeat;
	}

	void RegisterInteractiveBeat (InteractiveBeat interactiveBeat) {
		interactiveBeat.OnChangeTemporalDistance += CheckInteractiveBeatActivity;
	}
		
	void CheckInteractiveBeatActivity (InteractiveBeat interactiveBeat, float temporalDistanceFromBeat) {
		if (temporalDistanceFromBeat <= playSettings.timingWindowHalfWidth) {
			if (temporalDistanceFromBeat <= playSettings.timingWindowHalfWidth * -1) {
				interactiveBeat.OnChangeTemporalDistance -= CheckInteractiveBeatActivity;
				ActiveInteractiveBeats.Remove (interactiveBeat);
				if (OnDeactivateInteractiveBeat != null) {
					OnDeactivateInteractiveBeat (interactiveBeat);
					interactiveBeat.Destroy ();
				}
			} else {
				if (!ActiveInteractiveBeats.Contains (interactiveBeat)) {
					ActiveInteractiveBeats.Add (interactiveBeat);
					if (OnActivateInteractiveBeat != null)
						OnActivateInteractiveBeat (interactiveBeat);
				}
			}
		}
	}
}
