using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	private Character player;
	private PlatformManager platformManager;
	private ScoreManager scoreManager;
	public StartScreen startScreen;
	private Vector3 startPosition = Vector3.zero;
	
	void Start () {
		doReset();
	}
	
	void Awake() {
		startPosition = GameObject.FindGameObjectWithTag("Position-Start").transform.position;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		platformManager = this.GetComponent<PlatformManager>();
		scoreManager = this.GetComponent<ScoreManager>();
	}
	
	void Update () {
		// Force reset incase you get stuck...
		if(Input.GetKey(KeyCode.R)) {
			doReset();
		}
	
		handlePlayerOutOfBounds();
	}
	
	private void handlePlayerOutOfBounds() {
		// player out of bounds below a certain y-level
		if(player.transform.position.y < -10.0f) {
			doReset();
		}
	}
	
	public void doReset() {
		Debug.Log("reset");
		
		scoreManager.doReset();
		player.doReset(startPosition);
		platformManager.initialize(startPosition);
		
		startScreen.show();
	}
}
