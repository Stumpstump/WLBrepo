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
			running = 2,
			ledgegrab = 3,
			grabbingmoss = 4,
			wallclimb = 5,
			jumpingBlendTree = 6,
		}

		public AnimationState animationState;
		public Animator playerAnimator;

		public void SetState(AnimationState state)
		{
			int i = (int)state;
			playerAnimator.SetInteger ("State", i);
		}

		private void Update()
		{
			SetState (animationState);
		}
	}
}
