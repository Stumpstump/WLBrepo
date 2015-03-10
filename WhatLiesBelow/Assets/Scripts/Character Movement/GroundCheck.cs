using UnityEngine;
using System.Collections;

namespace WLB
{
	public class GroundCheck : MonoBehaviour 
	{
		public CharacterMotor characterMotor;

		private void OnTriggerEnter2D()
		{
			characterMotor.canJump = true;
		}
	}
}
