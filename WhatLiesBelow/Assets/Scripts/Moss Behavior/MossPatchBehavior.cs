using UnityEngine;
using System.Collections;

namespace WLB
{
	public class MossPatchBehavior : MonoBehaviour {

		public MossModule playerMossModule;
		public int mossAmount = 5;
		public int maxMoss = 5;
		public float refillDelay = 3f;
		public float playerRefillDelay = 1f;
		public Transform spawnPoint;

		private bool isRefilling = false;
		private bool canRefill = false;
		private bool isRefillingPlayer = false;

		private IEnumerator Refill()
		{
			yield return new WaitForSeconds (refillDelay);
			mossAmount++;
			isRefilling = false;
		}


		private IEnumerator RefillPlayer()
		{
			yield return new WaitForSeconds(playerRefillDelay);
			mossAmount -= 1;
			playerMossModule.mossCount++;
			isRefillingPlayer = false;
		}


		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.gameObject.tag == "Player")
			{
				canRefill = true;
			}
		}


		private void OnTriggerExit2D(Collider2D other)
		{
			if(other.gameObject.tag == "Player")
			{
				canRefill = false;
			}
		}


		private void FixedUpdate()
		{
			if(mossAmount < maxMoss && !isRefilling)
			{
				isRefilling = true;
				StartCoroutine(Refill ());
			}

			if(mossAmount > 0 &&
			   canRefill &&
			   !isRefillingPlayer &&
			   playerMossModule.mossCount < playerMossModule.maxMoss &&
			   Input.GetKey(KeyCode.X))
			{
				isRefillingPlayer = true;
				StartCoroutine(RefillPlayer());
			}
		}
	}
}
