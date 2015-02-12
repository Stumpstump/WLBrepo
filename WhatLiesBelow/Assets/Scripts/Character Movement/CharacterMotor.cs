using UnityEngine;
using System.Collections;


namespace WLB
{
	public class CharacterMotor : MonoBehaviour 
	{
		public float moveSpeed = 6f;
		public float sprintSpeed = 6f;
		public float jumpForce = 240f;
		public float fatigueLossRate = 1f;
		public float fatigueGainRate = 5f;
		public float fatigue = 100f;
		public bool fatigueEnabled;

		public Rigidbody2D rigidBody2D;
		public Transform playerPos;

		public bool canJump = true;
		public int moveDirection = 0;

		private bool isLoweringFatigue = false;
		private bool isSprinting = false;
		private bool isRaisingFatigue = false;

		//variables for jump length
		public float jumpSpeed = 200000f;
		public float gravity = 20000f;
		public float gravityForce = 30000f;
		public float airTime = 20000f;
		private float forceY = 0;
		private float invertGrav;

	//	private bool canDoubleJump = false;

	//	public bool CanDoubleJump
	//	{
	//		get { return canDoubleJump; }
	//		set
	//		{
	//			canDoubleJump = value;
	//		}
	//	}

		private IEnumerator drainFatigue()
		{
			yield return new WaitForSeconds (0.1f);
			fatigue -= fatigueLossRate;
			isLoweringFatigue = false;
		}


		private IEnumerator raiseFatigue()
		{
			yield return new WaitForSeconds (0.1f);
			fatigue += fatigueLossRate;
			isRaisingFatigue = false;
		}


		private void Update()
		{
			if (canJump) 
			{
				//Debug.Log ("canJump");
				//when grounded set forceY to 0
				forceY = 0;
				//invertGrav also reset when grounded
				invertGrav = gravity * airTime;
				if(Input.GetKey(KeyCode.W)){
					//Debug.Log ("Hit W");
					//Debug.Log ("Update Can Jump Status= " + canJump);
					forceY = jumpSpeed;
					//Debug.Log ("ForceY =" + forceY);
					//canJump = false;
				}
			}

			if(Input.GetKeyDown(KeyCode.LeftShift) && fatigue > 0)
			{
				isSprinting = true;
			}

			if(Input.GetKeyUp(KeyCode.LeftShift) || fatigue <= 0)
			{
				isSprinting = false;
			}

			if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				moveDirection = -1;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				playerPos.localScale = theScale;
				MovePlayer ();
			}

			if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				moveDirection = 1;
				Vector3 theScale = transform.localScale;
				theScale.x *= 1;
				playerPos.localScale = theScale;
				MovePlayer ();
			}

			if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y);
			}

			if(Input.GetKeyDown(KeyCode.W) && canJump)
			//if(Input.GetKeyDown(KeyCode.W))
			{
				Debug.Log ("Starting Jump");
				Debug.Log ("Hit W Can Jump Status= " + canJump);
				rigidBody2D.velocity = new Vector2(0f,0f);
				Jump();
				canJump = false;
			}

			if(!isSprinting && fatigue < 100f)
			{
				StartCoroutine(raiseFatigue());
				isRaisingFatigue = true;
			}
		}


		private void MovePlayer()
		{
			Vector2 move;
			if(isSprinting)
			{
				move = new Vector2 (playerPos.position.x + moveDirection * sprintSpeed * Time.deltaTime, 0);
				if(!isLoweringFatigue)
				{
					StartCoroutine(drainFatigue());
					isLoweringFatigue = true;
				}
			}
			else
			{
				move = new Vector2 (playerPos.position.x + moveDirection * moveSpeed * Time.deltaTime, 0);
			}

			playerPos.position = new Vector3(move.x, playerPos.position.y, 0);
		}

	/*	original jump
		private void Jump()
		{
			canJump = false;
			Vector2 move = new Vector2 (0 , jumpForce * 10);
			Debug.Log (move.y);
			rigidBody2D.AddForce (move);
		}

	*/

		private void Jump()
		{
			//canJump = false;
			Debug.Log ("In the jump");
			Debug.Log ("Jump function in the jump Can Jump Status= " + canJump);
			if(Input.GetKeyDown(KeyCode.W) && forceY != 0 ){
				Debug.Log ("invertGrav = "+invertGrav);
				invertGrav -= Time.deltaTime;
				Debug.Log ("forceY = "+forceY);
				forceY += invertGrav*Time.deltaTime;
				//Vector2 move = new Vector2 (0, forceY);
				//rigidBody2D.AddForce (move);
		}
			forceY -= gravity * Time.deltaTime * gravityForce;
			Debug.Log ("new " + forceY);
			Debug.Log ("Jump function Can Jump Status= " + canJump);
			//moveDirection.y = forceY;
			//problem is here. no force being applied!
			Vector2 move = new Vector2 (0, forceY);
			//Vector2 move = new Vector2 (0, moveDirection * Time.deltaTime);
			//rigidBody2D.AddForce (new Vector2(0,forceY));
			rigidBody2D.AddForce (move);
			canJump = true;
	}
}
}