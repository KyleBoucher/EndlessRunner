using UnityEngine;
using System.Collections;

/**
*	Trigger Handler
*	- Trigger occurs when a new platform is entered.
*	- Could be called ScoreTrigger or PlatformTrigger
*/
public class TriggerHandler : MonoBehaviour {

	private PlatformManager platformManager;
	private ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
		platformManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlatformManager>();
		scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("TRIGGER: Platform");
		scoreManager.increaseScore();
		// Create two platforms, higher speeds you can skip platforms
		// 		Possibly make this based on the speed
		//platformManager.createRandomPlatform(other.transform.position);
		
		int create = (int)(other.gameObject.GetComponent<Character>().currentSpeed);
		Debug.Log(create);
		for(int i = 0; i < (create <= 0 ? 1 : create); ++i) {
			platformManager.createRandomPlatform(other.transform.position);
		}
	}
}
