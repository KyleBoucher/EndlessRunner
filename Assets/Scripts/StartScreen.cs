using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {
	void Start() {
		show ();
	}

	void Update() {
		if(Input.GetKey(KeyCode.Escape)) {
			Debug.Log("Quitting...");
			Application.Quit();
		}
		
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			hide ();
		}
	}
	
	public void hide() {
		this.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
	public void show() {
		this.gameObject.SetActive(true);
		Time.timeScale = 0;
	}
	
}
