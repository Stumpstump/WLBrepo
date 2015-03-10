using UnityEngine;
using System.Collections;

namespace WLB
{
	public class GroundCheck : MonoBehaviour 
	{
		public CharacterMotor characterMotor;
		public bool isGrounded;

		private void OnTriggerEnter2D()
		{
			isGrounded = true;
			characterMotor.canJump = isGrounded;
		}

		private void OnTriggerExit2D()
		{
			isGrounded = false;
			characterMotor.canJump = isGrounded;
		}
	}
}
