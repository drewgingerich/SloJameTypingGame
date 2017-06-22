using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsScript : MonoBehaviour {

	void OnTriggerExit2D (Collider2D other) {
		other.gameObject.SetActive (false);
	}
}
