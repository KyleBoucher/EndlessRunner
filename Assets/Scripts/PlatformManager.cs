using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
*	Platform Manager
*	- Handles generation and placement of platforms (prefab)
*/
public class PlatformManager : MonoBehaviour {
	public Transform platform;
	
	public Vector2 gap; // Min/Max
	public Vector2 size; // Min/Max
	public Vector2 height; // Min/Max
	
	public float initialPlatformLength = 20f;
	public int initialPlatformBatchSize = 5;
	
	public float laneDistance = 3f;
	public float laneChance = 0.25f;
	public float leftRightChance = 0.5f;
	
	private Vector3 previousPosition = Vector3.zero;
	private float previousLength = 0;
	
	public void initialize(Vector3 startPosition) {
		// Clear all old platforms
		GameObject[] plats = GameObject.FindGameObjectsWithTag("Platform");
		Debug.Log("Cleaning " + plats.Length + " platforms");
		foreach(GameObject g in plats) {
			Destroy(g);
		}
		
		// generate the starting platform batch
		createStartPlatform(initialPlatformLength, startPosition - Vector3.up - Vector3.forward);
		for(int i = 0; i < initialPlatformBatchSize; ++i) {
			createRandomPlatform(startPosition);
		}
	}
	
	public void createStartPlatform(float length, Vector3 start) {
		createPlatform(length, start, start);
	}
	
	public void createRandomPlatform(Vector3 pos) {
	Debug.Log("Create Random Platofrm");
		laneChance = Mathf.Min(1.0f, laneChance + 0.01f);
		
		int make = Random.Range(1,5);//min-Inclusive, max-exclusive
		
		float s = Random.Range(size.x, size.y);
		//TODO: Cant jump max distance and max height
		Vector3 p = previousPosition + Vector3.forward*previousLength // Past the last platform
						+ Vector3.forward * Random.Range(gap.x, gap.y) // Some random gap away
						+ Vector3.up * Random.Range(height.x, height.y); // Some random height offset
		
		List<Vector3> lane = new List<Vector3>();
		switch(make) {
			case 1: // fall through
			case 2: {
				lane.Add(Vector3.right * (Random.Range(0.0f, 1.0f) < laneChance 
			                        ? laneDistance * (Random.Range(0.0f, 1.0f) < leftRightChance 
		                  				? 1 : -1) 
			                        : 0));
			}break;
			case 3: {
				// avoid one direction bias, as createPlatform() stores previous position
				if(Random.Range(0.0f, 1.0f) < 0.5) {
					lane.Add(Vector3.right * -laneDistance);
					lane.Add(Vector3.right * laneDistance);
				} else {
					lane.Add(Vector3.right * laneDistance);
					lane.Add(Vector3.right * -laneDistance);
				}
			}break;
			case 4: {
				lane.Add(Vector3.right * laneDistance);
				lane.Add(Vector3.right * -laneDistance);
				lane.Add(Vector3.zero);
			}break;
		}
		
		for(int i = 0; i < (make==1?make:make-1); ++i) {//make-1 due to extra fallthrough of case1&2
			createPlatform(s, pos + Vector3.down * 3f 
								  + Vector3.right * (Random.Range(0.0f, 1.0f)<=0.5f?5f:-5f), 
							  p + lane[i] ); // Random lane change
		}
	}
	
	Transform createPlatform(float length, Vector3 startPosition, Vector3 targetPosition) {
		Transform obj = (Transform)Instantiate(platform, startPosition, Quaternion.identity);
		obj.Find("ScalePlatform").transform.localScale = new Vector3(1, 1, length); // Scale for length of platform
		
		// "this" is the LevelManager object, less clutter on the editor
		obj.SetParent(this.transform);
		
		obj.GetComponent<Platform>().setTarget(targetPosition);
		
		previousPosition = targetPosition;
		previousLength = length;
		
		return obj;
	}
}

