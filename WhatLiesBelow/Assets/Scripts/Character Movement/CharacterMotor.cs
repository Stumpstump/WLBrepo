using UnityEngine;
using System.Collections;


namespace WLB
{
	public class CharacterMotor : MonoBehaviour 
	{
		public float vSpeed;
		public float moveSpeed = 6f;
		public float sprintSpeed = 6f;
		public float jumpForce = 240f;
		public float jumpForceAdditive = 7f;
		public float jumpForceAdditiveDuration = 0.5f;
		public float fatigueLossRate = 1f;
		public float fatigueGainRate = 5f;
		public float fatigue = 100f;
		public bool fatigueEnabled;

		public AnimationModule animationModule;
		public LedgeGrabMotor ledgeGrabMotor;
		public WallRunMotor wallRunMotor;


		public Rigidbody2D rigidBody2D;
		public Transform playerPos;

		public bool canJump = true;
		public bool isJumping = false;
		public int moveDirection = 0;

		private bool isLoweringFatigue = false;
		private bool isSprinting = false;
		private bool isRaisingFatigue = false;
		private float axisNumber;

		//for the jump code
		private float currentTime = 0f;
		private Vector2 move;
		private Vector2 addedForceVector;

//<<<<<<< HEAD
//=======

//<<<<<<< HEAD
//>>>>>>> 6bf4137615947a9810a70055485dfea1b95148d2
	//	private bool canDoubleJump = false;

	//	public bool CanDoubleJump
	//	{
	//		get { return canDoubleJump; }
	//		set
	//		{
	//			canDoubleJump = value;
	//		}
	//	}

//=======
//>>>>>>> master
		private IEnumerator drainFatigue()
		{
			yield return new WaitForSeconds (0.1f);
			fatigue -= fatigueLossRate;
			isLoweringFatigue = false;
		}


		private IEnumerator raiseFatigue()
		{
			yield return new WaitForSeconds (0.1f);
			fatigue += fatigueLossRate;
			isRaisingFatigue = false;
		}


		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.LeftShift) && fatigue > 0)
			{
				isSprinting = true;
			}

			if(Input.GetKeyUp(KeyCode.LeftShift) || fatigue <= 0)
			{
				isSprinting = false;
			}
			if(!Input.GetKey(KeyCode.A) &&
			   !Input.GetKey (KeyCode.D) &&
			   !Input.GetKey(KeyCode.LeftArrow) &&
			   !Input.GetKey(KeyCode.RightArrow))
			{
				axisNumber = 0;
			}
			else if((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || 
			        (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
			{
				axisNumber = 0;
			}
			else
			{
				axisNumber = Input.GetAxis("Horizontal");
			}

			if (axisNumber == 0) 
			{
				rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, rigidBody2D.velocity.y);
				animationModule.SetState (AnimationModule.AnimationState.idle1);
				Debug.Log("this shit is happening");
			} else if (axisNumber < 0) {
				moveDirection = -1;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				playerPos.localScale = theScale;
				MovePlayer ();
				if (!isSprinting && wallRunMotor.canWallRun && !ledgeGrabMotor.isClimbing && canJump) {
					animationModule.SetState (AnimationModule.AnimationState.walking);
				} else if (wallRunMotor.canWallRun && !ledgeGrabMotor.isClimbing && canJump) {
					animationModule.SetState (AnimationModule.AnimationState.running);
				}
			} else if (axisNumber > 0) {
				moveDirection = 1;
				Vector3 theScale = transform.localScale;
				theScale.x *= 1;
				playerPos.localScale = theScale;
				MovePlayer ();
				if (!isSprinting && wallRunMotor.canWallRun && !ledgeGrabMotor.isClimbing && canJump) {
					animationModule.SetState (AnimationModule.AnimationState.walking);
				} else if (wallRunMotor.canWallRun && !ledgeGrabMotor.isClimbing && canJump) {
					animationModule.SetState (AnimationModule.AnimationState.running);
				}
			}

			if((Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)) && canJump)
			{
				//sets momentum to 0 so it can't cancel the jump
				rigidBody2D.velocity = new Vector2(0f,0f);
				rigidBody2D.AddForce (move);
				//run jump function
//<<<<<<< HEAD
				AudioHelper.CreatePlayAudioObject(SoundLibrary.water2);
//=======
				animationModule.SetState(AnimationModule.AnimationState.jumpingBlendTree);
//<<<<<<< HEAD
//>>>>>>> 6bf4137615947a9810a70055485dfea1b95148d2
				//StartCoroutine(Jump());
//=======
				currentTime = 0;
				isJumping = true;
//>>>>>>> master
				//set canJump to false to disallow infinite jumping
				canJump = false;
			}

			if((Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)) && !canJump)
			{
				animationModule.SetState(AnimationModule.AnimationState.jumpingBlendTree);
			}

			if(!isSprinting && fatigue < 100f)
			{
				StartCoroutine(raiseFatigue());
				isRaisingFatigue = true;
			}

			if(!wallRunMotor.canWallReset)
			{
				rigidBody2D.velocity = new Vector2(0f,0f);
			}
		}


		private void FixedUpdate()
		{
			if(isJumping)
			{
				if(currentTime < jumpForceAdditiveDuration && Input.GetKey(KeyCode.W))
				{
					//animationModule.SetState(AnimationModule.AnimationState.jumpingBlendTree);
					rigidBody2D.AddForce (addedForceVector);
					currentTime += Time.deltaTime;
				}
				else
				{
					isJumping = false;
				}
			}
			vSpeed = rigidBody2D.velocity.y;
			//I need this for my blendtree to work. Refference unity video at 58:20
		}


		private void MovePlayer()
		{
			Vector2 move;
			if(isSprinting)
			{
				move = new Vector2 (playerPos.position.x + moveDirection * sprintSpeed * Time.deltaTime, 0);
				if(!isLoweringFatigue)
				{
					StartCoroutine(drainFatigue());
					isLoweringFatigue = true;
				}
			}
			else
			{
				move = new Vector2 (playerPos.position.x + moveDirection * moveSpeed * Time.deltaTime, 0);
			}

			playerPos.position = new Vector3(move.x, playerPos.position.y, 0);
		}


		private void Start()
		{
			move = new Vector2 (0 , jumpForce * 10);
			addedForceVector = new Vector2 (0, jumpForceAdditive);
		}
	}
}