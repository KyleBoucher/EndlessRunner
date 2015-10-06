using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public Vector3 targetPos = Vector3.zero;
	public Vector3 currentPos = Vector3.zero;
	
	public Vector3 timeToAlign = Vector3.one;
	
	public enum State {
		X,Y,Z,DONE
	}
	public State curMoving = State.Z;
	
	
	public void setTarget(Vector3 pos) {
		targetPos = pos;
		curMoving = State.Z;
	}
	
	void Update() {
		if(curMoving == State.DONE) {
			transform.position = targetPos;
			return;
		}
	
		currentPos = transform.position;
		float coord = 0;
		float targetCoord = 0;
		float time = 0;
		
		switch(curMoving) {
			case State.X: {
				coord = currentPos.x;
				targetCoord = targetPos.x;
				time = timeToAlign.x;
			}break;
			case State.Y: {
				coord = currentPos.y;
				targetCoord = targetPos.y;
				time = timeToAlign.y;
			}break;
			case State.Z: {
				coord = currentPos.z;
				targetCoord = targetPos.z;
				time = timeToAlign.z;
			}break;
		}
		
		if(Mathf.Abs(targetCoord - coord) >= 0.05) {
			coord = Mathf.Lerp(coord, targetCoord, Time.deltaTime * (1.0f/time));
			switch(curMoving) {
			case State.X: {currentPos.x = coord;}break;
			case State.Y: {currentPos.y = coord;}break;
			case State.Z: {currentPos.z = coord;}break;
			}
		} else {
			coord = targetCoord;
			switch(curMoving) {
			case State.X: {currentPos.x = coord;}break;
			case State.Y: {currentPos.y = coord;}break;
			case State.Z: {currentPos.z = coord;}break;
			}
			
			switch(curMoving) {
				case State.X: {curMoving = State.Y;}break;
				case State.Y: {curMoving = State.DONE;}break;
				case State.Z: {curMoving = State.X;}break;
			}
		}
		
		transform.position = currentPos;
		
	}
}
