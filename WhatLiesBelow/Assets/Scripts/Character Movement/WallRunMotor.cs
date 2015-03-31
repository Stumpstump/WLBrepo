using UnityEngine;
using System.Collections;

namespace WLB
{
	public class WallRunMotor : MonoBehaviour {

		public float climbTime;
		public CharacterMotor characterMotor;

		public Transform playerPos;
		public Transform wallRunPos;
		
		public Collider2D spaceCheck;
		public Collider2D ledgeCheck;

		public bool isLedge;
		public bool spaceEmpty;

		public GroundCheck groundCheck;
		public LedgeGrabMotor ledgeGrabMotor;

		private bool canWallRun = true;

		private void Update()
		{
			if(isLedge && !spaceEmpty && canWallRun && !groundCheck.isGrounded)
			{
				Debug.Log ("launching wall run");
				canWallRun = false;
				StartCoroutine(WallRun());
			}

			if(!canWallRun && groundCheck.isGrounded)
			{
				canWallRun = true;
			}
		}


		private IEnumerator WallRun()
		{
			bool doneRunning = false;

			if (ledgeGrabMotor.isClimbing) doneRunning = true;

			float elapsedTime = 0f;
			Vector2 endPos = wallRunPos.position;
			Vector2 currentPos = playerPos.position;
			Debug.Log ("wall  running");
			while (elapsedTime < climbTime)
			{
				characterMotor.canJump = true;
				doneRunning = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || ledgeGrabMotor.isClimbing;
				if(doneRunning) break;

				elapsedTime += Time.deltaTime;
				playerPos.position = Vector2.Lerp(currentPos, endPos, (elapsedTime / climbTime));
				yield return null;
			}

			while(!doneRunning)
			{
				characterMotor.canJump = true;
				doneRunning = Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.S) || ledgeGrabMotor.isClimbing;
				if(doneRunning) break;

				playerPos.position = endPos;
				yield return null;
			}
		}
	}
}
