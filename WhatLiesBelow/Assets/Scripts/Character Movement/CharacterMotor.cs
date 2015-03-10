using UnityEngine;
using System.Collections;


namespace WLB
{
	public class CharacterMotor : MonoBehaviour 
	{
		public float moveSpeed = 6f;
		public float sprintSpeed = 6f;
		public float jumpForce = 350f;
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
		public float jumpSpeed = 500.0f;
		public float gravity = 20.0f;
		public float gravityForce = 300.0f;
		public float airTime = 2f;
		//was private
		public float forceY = 0;
		//was private
		public float invertGrav;

	//	private bool canDoubleJump = false;

	//	public bool CanDoubleJump
	//	{
	//		get { return canDoubleJump; }
	//		set
	//		{
	//			canDoubleJump = value;
	//		}
	//	}

		private void Start()
		{
			invertGrav = gravity * airTime;
			canJump = true;
		}

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
			Debug.Log ("forceY = " + forceY);
			forceY -= gravity * Time.deltaTime * gravityForce;
			Vector2 moveGuy = new Vector2 (0, forceY);
			rigidBody2D.AddForce (moveGuy);
			if (canJump) 
			{
				//when grounded set forceY to 0
				//forceY = 0;
				//invertGrav also reset when grounded
				//invertGrav = gravity * airTime;
				if(Input.GetKeyDown(KeyCode.W)){
					//sets forceY to jumpSpeed to allow the jump
					forceY = 0;
					invertGrav = gravity * airTime;
					forceY = jumpSpeed;
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
			{
				//sets momentum to 0 so it can't cancel the jump
				rigidBody2D.velocity = new Vector2(0f,0f);
				//run jump function
				//Jump();
				canJump = false;
				if(Input.GetKey(KeyCode.W) && forceY > 0 ){
					//allow a jump that will gradually increase over time
					invertGrav -= Time.deltaTime;
					forceY += invertGrav*Time.deltaTime;
				}
				//set canJump to false to disallow infinite jumping
				//canJump = false;		moved to Jump function
				//forceY -= gravity * Time.deltaTime * gravityForce;
				//set the move variable to the right speed
				//Vector2 moveGuy = new Vector2 (0, forceY);
				//apply the jump force
				//rigidBody2D.AddForce (moveGuy);
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
			//set canJump to false to disallow infinite jumping
			canJump = false;
			if(Input.GetKeyDown(KeyCode.W) && forceY != 0 ){
				//allow a jump that will gradually increase over time
				invertGrav -= Time.deltaTime;
				forceY += invertGrav*Time.deltaTime;
		}
/*			forceY -= gravity * Time.deltaTime * gravityForce;
			//set the move variable to the right speed
			Vector2 move = new Vector2 (0, forceY);
			//apply the jump force
			rigidBody2D.AddForce (move);
			//allow jumping again after you land
			//canJump = true;
*/
	}
}
}