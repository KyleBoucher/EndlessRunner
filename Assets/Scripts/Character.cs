using UnityEngine;
using System.Collections;

/// <summary>
/// Character.
/// - Handles inputs and movement
/// </summary>
public class Character : MonoBehaviour {

	private CharacterAnimController animCont;
	private Rigidbody rb;
	private CapsuleCollider capsule;
	
	public LayerMask mask;
	public float initialMoveSpeed = 1f;
	public float jumpForce = 50f;
	public float groundDistance_g = 0.1f;
	public float groundDistance_a = 0.1f;
	private bool isGrounded = true;
	private Vector3 move = Vector3.zero;
	
	public float groundDist;
	private float capsuleHeightOrig = 0;
	private Vector3 capsuleCenterOrig = Vector3.zero;
	
	public bool changingLane = false;
	public float move_startPos = 0;
	public float move_endPos = 0;
	public float laneDist = 0;
	
	public float maxMoveSpeed = 5f;
	public float moveSpeedIncrease = 0.01f; // Per second
	public float slowSpeed = 0.5f;
	private float slowSpeedModifier = 1.0f;
	//[HideInInspector]
	public float currentSpeed;
	
	void Awake() {
		animCont = GetComponent<CharacterAnimController>();
		rb = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		
		capsuleHeightOrig = capsule.height;
		capsuleCenterOrig = capsule.center;
		groundDist = groundDistance_g;
	}
	
	void Start() {
		PlatformManager platMan = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<PlatformManager>();
		laneDist = platMan.laneDistance;
		
		currentSpeed = initialMoveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		move.x = move.y = move.z = 0;
		
//		move = transform.InverseTransformDirection(move);
		checkGrounded();
//		move = Vector3.ProjectOnPlane(move, Vector3.up);
		
		handleJump();
		handleSlowSpeed();
		
		move.z = currentSpeed * slowSpeedModifier;
		move.y = rb.velocity.y;
		
		animCont.updateAnim(move, isGrounded);
		
		if(changingLane) {
			// interpolate from current position to new lane
			float x =  Mathf.Lerp(transform.position.x, move_endPos, Time.deltaTime*laneDist);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			// stop when close enough, and set position
			if(Mathf.Abs(move_endPos - x) < 0.05f) {
				changingLane = false;
				transform.position = new Vector3(move_endPos, transform.position.y, transform.position.z);
			}
		}
		
		// increase move speed to make it more interesting
		if(currentSpeed < maxMoveSpeed) {
			currentSpeed += moveSpeedIncrease * Time.deltaTime;
		} else if(currentSpeed > maxMoveSpeed) {
			currentSpeed = maxMoveSpeed;
		}
	}
	
	private void handleSlowSpeed() {
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			slowSpeedModifier = slowSpeed;
		} else {
			slowSpeedModifier = 1.0f;
		}
	}
	
	private void handleJump() {
		if(isGrounded && animCont.anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded")) {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
				doJump();
			}
			
			/////// CHANGE TO AUTO JUMP WHEN CHANGING LANES
			// Change lane - left
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
				changingLane = true;
				move_startPos = transform.position.x;
				move_endPos = move_startPos - laneDist;
				doJump();
			} // Change lane - right
			else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
				changingLane = true;
				move_startPos = transform.position.x;
				move_endPos = move_startPos + laneDist;
				doJump();
			}
		}
	}
	
	private void doJump() {
		rb.velocity = new Vector3(rb.velocity.x,jumpForce,rb.velocity.z);
	}
	
	private void checkGrounded() {
#if UNITY_EDITOR
		// Debugging line for RayCast
		Debug.DrawLine(transform.position + capsule.center, transform.position + capsule.center + (Vector3.down * groundDist));
#endif
		
		RaycastHit hitInfo;
		// Raycasting down checks for a ground - Check for Groundedness
		if (Physics.Raycast(transform.position + capsule.center, Vector3.down, out hitInfo, groundDist, mask)) {
			
			if(false == isGrounded) {
				// Reset physics capsule height and position
				capsule.height = capsuleHeightOrig;
				capsule.center = capsuleCenterOrig;
			}
			
			isGrounded = true;
			groundDist = groundDistance_g;
		} else {
			if(false != isGrounded) {
				// change the physics capsule height and position to somewhat match the animation
				capsule.height = capsuleHeightOrig * 0.75f;
				capsule.center = capsuleCenterOrig * 1.25f;
			}
			
			isGrounded = false;
			groundDist = groundDistance_a;
		}
	}
	
	/// <summary>
	/// Handles the resetting of the character
	/// </summary>
	/// <param name="pos">Starting Position.</param>
	public void doReset(Vector3 pos) {
		isGrounded = true;
		groundDist = groundDistance_g;
		capsule.height = capsuleHeightOrig;
		capsule.center = capsuleCenterOrig;
		rb.velocity = Vector3.zero;
		currentSpeed = initialMoveSpeed;
		
		changingLane = false;
		
		this.gameObject.transform.position = pos;
	}
}

