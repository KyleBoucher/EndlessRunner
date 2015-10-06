using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
*	Score Manager
*	- Handles display and updating of score
*/
public class ScoreManager : MonoBehaviour {
	public Text scoreDisplay;
	
	public int previousScore = 0;
	public int score = 0;
	
	public void doReset() {
		previousScore = score;
		score = 0;
		updateDisplay();
	}
	public void increaseScore() {
		score++;
		updateDisplay();
	}
	
	void updateDisplay() {
		scoreDisplay.text = "Previous: " + previousScore + "\nCurrent : " + score;
	}
}
