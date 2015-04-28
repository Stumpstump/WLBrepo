using UnityEngine;
using System.Collections;

namespace WLB
{
	public class WallRunMotor : MonoBehaviour {
		
		public float climbTime;
		public float climbSpeed = 5f;
		public float cutoffSpeed = 0.9f;
		public CharacterMotor characterMotor;
		public AnimationModule animationModule;

		public Transform playerPos;
		public Transform wallRunPos;
		public float climbHeight = 2f;
		
		public SpaceCheck spaceCheck;
		public LedgeCheck ledgeCheck;
		public GroundCheck groundCheck;

		public LedgeGrabMotor ledgeGrabMotor;

		public bool canWallRun = true;
		public bool canWallReset = true;

		private void Update()
		{
			if(ledgeCheck.IsLedge && !spaceCheck.IsEmpty && canWallRun && !groundCheck.isGrounded)
			{
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
			int breakCode = DetectBreakInput();

			if (ledgeGrabMotor.isClimbing || Input.GetAxisRaw ("Horizontal") == breakCode) doneRunning = true;

			float elapsedTime = 0f;
			Vector2 endPos = new Vector2(playerPos.position.x, (playerPos.position.y + climbHeight));
			Vector2 currentPos = playerPos.position;

			while (elapsedTime < climbTime)
			{
				animationModule.SetState(AnimationModule.AnimationState.wallclimb);
				characterMotor.canJump = true;
				doneRunning = Input.GetAxisRaw("Vertical") == -1 || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || ledgeGrabMotor.isClimbing || Input.GetAxisRaw ("Horizontal") == breakCode;
				if(doneRunning) break;

				elapsedTime += Time.deltaTime;
				float effectiveSpeed = Mathf.Lerp(climbSpeed, cutoffSpeed, (elapsedTime/climbTime));

				if(effectiveSpeed <= 1f) doneRunning = true;

				playerPos.position = Vector2.Lerp(currentPos, endPos, effectiveSpeed * (elapsedTime/climbTime));
				yield return null;
			}

			characterMotor.rigidBody2D.velocity = Vector2.zero;
			animationModule.SetState (AnimationModule.AnimationState.jumpingBlendTree); 
			canWallReset = true;
		}


		private float Smoothstepper(float edge0, float edge1, float x)
		{
			// Scale, bias and saturate x to 0..1 range
			x = Mathf.Clamp((x - edge0)/(edge1 - edge0), 0.0f, 1.0f); 
			// Evaluate polynomial
			return x*x*(3 - 2*x);
		}


		public int DetectBreakInput()
		{
			if (Input.GetAxisRaw("Horizontal") > 0)
				return -1;

			if (Input.GetAxisRaw("Horizontal") < 0)
				return 1;

			return 0;
		}
	}
}
