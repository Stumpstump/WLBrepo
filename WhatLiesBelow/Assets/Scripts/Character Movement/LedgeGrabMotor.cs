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

		public Collider2D spaceCheck;
		public Collider2D ledgeCheck;

		public List<GameObject> gosInSpaceCheck = new List<GameObject>();
		public bool isLedge = false;
		public bool spaceEmpty = false;

		private bool isClimbing;

		private void Update()
		{
			Debug.Log ("isLedge " + isLedge + " spaceEmpty " + spaceEmpty);
			if(isLedge && spaceEmpty && !isClimbing)
			{
				StartCoroutine(Climb());
			}
		}


		private IEnumerator Climb()
		{
			isClimbing = true;
			Rigidbody2D rb2d = playerPos.gameObject.GetComponent<Rigidbody2D> ();
			//rb2d.isKinematic = false;

			Vector2 endPos = climbPos.position;
			Vector2 currentPos = playerPos.position;
			Debug.Log ("climbing");

			float elapsedTime = 0f;

			while(elapsedTime < climbTime)
			{
				playerPos.position = Vector2.Lerp(currentPos, endPos, (elapsedTime / climbTime));
				elapsedTime += Time.deltaTime;
				Debug.Log(playerPos.position);
				yield return null;
			}
			playerPos.position = endPos;

			//rb2d.isKinematic = true;
			isClimbing = false;
		}
	}
}
