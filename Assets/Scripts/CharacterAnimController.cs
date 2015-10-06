using UnityEngine;
using System.Collections;

/// <summary>
/// Character animation controller.
///	- Handles animation specific values
/// </summary>
public class CharacterAnimController : MonoBehaviour {
	public Animator anim;
	
	public void updateAnim(Vector3 move, bool grounded) {
		anim.SetBool("OnGround", grounded);
	
		if(false == grounded) {
			anim.applyRootMotion = false; // stop root motion during jump/falling
			anim.SetFloat("Jump", move.y);
		} else {
			//anim.SetFloat("Jump", 0);
			anim.applyRootMotion = true; // reapply root motion when on ground
		}
		
		anim.SetFloat("Forward", move.z, 0.1f, Time.deltaTime); 
		if(move.z > 1) { 
			anim.speed = move.z; 
		} 
	}
}
