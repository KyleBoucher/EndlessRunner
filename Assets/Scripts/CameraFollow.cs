using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;
	public Vector3 lookOffset = new Vector3(0, 0, 2);
	public Vector3 followOffset = new Vector3(0, 4, -9);
	public float smoothTime = 0.01f;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = toFollow.position + followOffset;
		Vector3 v = Vector3.zero;
		transform.position = Vector3.SmoothDamp(transform.position, pos, ref v, smoothTime);
		transform.LookAt(toFollow.position + lookOffset);
	}
}
