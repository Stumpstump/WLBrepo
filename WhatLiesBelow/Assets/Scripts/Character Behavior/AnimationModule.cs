using UnityEngine;
using System.Collections;

namespace WLB
{
	public class AnimationModule : MonoBehaviour 
	{
		public enum AnimationState : int
		{
			idle1 = 0,
			walking = 1,
			running = 3,
			ledgegrab = 2,
			grabbingmoss = 6,
			wallclimb = 4,
			jumpingBlendTree = 5,
		}

		public AnimationState currentState;

		public AnimationState animationState;
		public Animator playerAnimator;

		public void SetState(AnimationState state)
		{
			//if(state != currentState)
			//{
			currentState = state;
			int i = (int)state;
			playerAnimator.SetInteger ("state", i);
			//}
		}

		private void Update()
		{
			//SetState (animationState);
		}
	}
}
