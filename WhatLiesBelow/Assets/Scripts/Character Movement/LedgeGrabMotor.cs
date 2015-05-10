using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class LedgeGrabMotor : MonoBehaviour 
	{
		public float climbTime;

		public Transform playerPos;
		public Transform climbPos;

		public AnimationModule animationModule;

		public SpaceCheck spaceCheck;
		public LedgeCheck ledgeCheck;
		public GroundCheck groundCheck;
	
		public bool isClimbing;
		private bool canClimb = true;

		private void Update()
		{
			if(ledgeCheck.IsLedge && spaceCheck.IsEmpty && !isClimbing && canClimb)
			{
				Debug.Log ("climbing");
				isClimbing = true;
				animationModule.SetState(AnimationModule.AnimationState.ledgegrab);
				StartCoroutine(Climb());
			}

			if(!canClimb && groundCheck.isGrounded)
			{
				canClimb = true;
			}
		}


		private IEnumerator Climb()
		{
			isClimbing = true;
			Rigidbody2D rb2d = playerPos.gameObject.GetComponent<Rigidbody2D> ();

			Vector2 endPos = climbPos.position;
			Vector2 currentPos = playerPos.position;
			//Debug.Log ("climbing");

			float elapsedTime = 0f;



			while(elapsedTime < climbTime)
			{
				animationModule.SetState(AnimationModule.AnimationState.ledgegrab);
				playerPos.position = Vector2.Lerp(currentPos, endPos, (elapsedTime / climbTime));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			playerPos.position = endPos;

			//rb2d.isKinematic = true;
			isClimbing = false;
			canClimb = false;
		}
	}
}
