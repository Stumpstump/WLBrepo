using UnityEngine;
using System.Collections;

namespace WLB
{
	public class AnimationModule : MonoBehaviour 
	{
		public enum AnimationState : int
		{
			walking = 0,
			running = 1,
			idle1 = 2,
			idle2 = 3,
			jump0 = 4,
			jump1 = 5,
			ledgegrab = 6,
			grabbingmoss = 7,
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
