using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedTextUpdater : MonoBehaviour {
	public Character character;
	public Text text;
	// Update is called once per frame
	void Update () {
		text.text = "Speed: " + character.currentSpeed.ToString("F2");
	}
}
