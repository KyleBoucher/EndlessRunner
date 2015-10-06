using UnityEngine;
using System.Collections;

/// <summary>
/// Aligns the collider to the center of the platform
///		This is very ugly and snappy in the game, 
/// 	consider interpolating to the position rather than snapping.
/// </summary>
public class Alignment : MonoBehaviour {

	public Transform alignTo;

	void OnCollisionEnter(Collision col) {
		col.collider.transform.position = new Vector3(alignTo.position.x, col.collider.transform.position.y, col.collider.transform.position.z);
	}
	void OnCollisionStay(Collision col) {
		col.collider.transform.position = new Vector3(alignTo.position.x, col.collider.transform.position.y, col.collider.transform.position.z);
	}
}
