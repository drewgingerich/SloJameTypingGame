using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTestBehavior : MonoBehaviour {

	void Start () {
		Text text = this.gameObject.GetComponent<Text> ();
		text.text = "testing123\ntesting123";
	}
}
