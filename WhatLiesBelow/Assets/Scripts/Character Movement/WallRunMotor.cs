﻿using UnityEngine;
using System.Collections;

namespace WLB
{
	public class WallRunMotor : MonoBehaviour {

		public float climbTime;
		public CharacterMotor characterMotor;
		public AnimationModule animationModule;

		public Transform playerPos;
		public Transform wallRunPos;
		public float climbHeight = 2f;
		
		public Collider2D spaceCheck;
		public Collider2D ledgeCheck;

		public bool isLedge;
		public bool spaceEmpty;

		public GroundCheck groundCheck;
		public LedgeGrabMotor ledgeGrabMotor;

		public bool canWallRun = true;
		public bool canWallReset = true;

		private void Update()
		{
			if(isLedge && !spaceEmpty && canWallRun && !groundCheck.isGrounded)
			{
				Debug.Log ("launching wall run");
				canWallRun = false;
				animationModule.SetState(AnimationModule.AnimationState.wallclimb);
				canWallReset = false;
				StartCoroutine(WallRun());
			}

			if(!canWallRun && groundCheck.isGrounded && canWallReset)
			{
				canWallRun = true;
			}
		}


		private IEnumerator WallRun()
		{
			bool doneRunning = false;
			KeyCode breakCode = DetectBreakInput();

			if (ledgeGrabMotor.isClimbing || Input.GetKey(breakCode)) doneRunning = true;

			float elapsedTime = 0f;
			//Vector2 endPos = wallRunPos.position;
			Vector2 endPos = new Vector2(playerPos.position.x, (playerPos.position.y + climbHeight));
			Vector2 currentPos = playerPos.position;
			Debug.Log (currentPos + " : " + endPos);
			Debug.Log (climbHeight);

			while (elapsedTime < climbTime)
			{
				animationModule.SetState(AnimationModule.AnimationState.wallclimb);
				characterMotor.canJump = true;
				doneRunning = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || ledgeGrabMotor.isClimbing || Input.GetKey(breakCode);
				if(doneRunning) 
				{
					//animationModule.SetState(AnimationModule.AnimationState.idle1);
					break;
				}

				elapsedTime += Time.deltaTime;
				playerPos.position = Vector2.Lerp(currentPos, endPos, (elapsedTime / climbTime));
				Debug.Log (playerPos.position);
				Debug.Log (Vector2.Lerp(currentPos, endPos, (elapsedTime / climbTime)));
				Debug.Log (endPos);
				yield return null;
			}
			Debug.Log ("made it here");
			while(!doneRunning)
			{
				animationModule.SetState(AnimationModule.AnimationState.wallclimb);
				characterMotor.canJump = true;
				doneRunning = Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.S) || ledgeGrabMotor.isClimbing || Input.GetKey(breakCode);
				if(doneRunning) 
				{
					//animationModule.SetState(AnimationModule.AnimationState.idle1);
					break;
				};

				playerPos.position = endPos;
				yield return null;
			}

			characterMotor.rigidBody2D.velocity = Vector2.zero;
			animationModule.SetState (AnimationModule.AnimationState.jumpingBlendTree); 
			canWallReset = true;
		}

		public KeyCode DetectBreakInput()
		{
			if (Input.GetKey (KeyCode.A))
				return KeyCode.D;

			if (Input.GetKey (KeyCode.D))
				return KeyCode.A;

			return KeyCode.Ampersand;
		}
	}
}
