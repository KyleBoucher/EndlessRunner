using UnityEngine;
using System.Collections;

public class OffScreenDestructor : MonoBehaviour {

	public GameObject destroyMe;
	
	/// <summary>
	/// Raises the became invisible event.
	/// </summary>
	void OnBecameInvisible() {
		Invoke("completeDestroy", 1.0f);
	}
	
	/// <summary>
	/// Raises the became visible event.
	/// </summary>
	void OnBecameVisible() {
		CancelInvoke("completeDestroy");
	}
	
	/// <summary>
	/// Completes the destroy of required gameobject.
	/// </summary>
	void completeDestroy() {
		Destroy(destroyMe);
	}
}
