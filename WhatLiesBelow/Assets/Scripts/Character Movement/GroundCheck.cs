using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class GroundCheck : MonoBehaviour 
	{
		public CharacterMotor characterMotor;
		public bool isGrounded;

		private List<GameObject> groundCheckObjects = new List<GameObject>(0);

		private void OnTriggerEnter2D(Collider2D other)
		{
			isGrounded = true;
			characterMotor.canJump = isGrounded;

			bool passes = true;
			foreach(GameObject go in groundCheckObjects)
			{
				if(go == other.gameObject)
				{
					passes = false;
					break;
				}
			}

			if(passes) groundCheckObjects.Add (other.gameObject);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			for( int i = 0; i < groundCheckObjects.Count; i++)
			{
				if(groundCheckObjects[i] == other.gameObject)
				{
					groundCheckObjects.RemoveAt(i);
					break;
				}
			}

			if(groundCheckObjects.Count == 0)
			{
				isGrounded = false;
				characterMotor.canJump = isGrounded;
			}
		}
	}
}
