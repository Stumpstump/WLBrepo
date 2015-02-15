using UnityEngine;
using System.Collections;

namespace WLB
{
	public class DeathModule : MonoBehaviour 
	{
		public Rigidbody2D player;
		public MossModule mossModule;
		public SpriteRenderer fadeSprite;
		public float fadeOutTime;
		public float fadeInTime;
		public Transform spawnPoint;



		//public Transform SpawnPoint
		//{
		//	get{ return spawnPoint; }
		//	set{ spawnPoint = value; }
		//}


		public void KillPlayer()
		{
			StartCoroutine (Die ());
		}


		private IEnumerator Die()
		{
			float timeElapsed = 0f;
			while(timeElapsed < fadeOutTime)
			{
				fadeSprite.color = new Color(fadeSprite.color.r,
											 fadeSprite.color.g,
											 fadeSprite.color.b,
											 timeElapsed/fadeOutTime);

				timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			fadeSprite.color = new Color(fadeSprite.color.r,
			                             fadeSprite.color.g,
			                             fadeSprite.color.b,
			                             1f);

			yield return new WaitForSeconds (fadeOutTime/2f);

			StartCoroutine (Respawn ());
		}


		private IEnumerator Respawn()
		{
			player.velocity = Vector2.zero;
			player.transform.position = spawnPoint.position;
			mossModule.mossCount = mossModule.startMossCount;

			float timeElapsed = 0f;
			while(timeElapsed < fadeInTime)
			{
				fadeSprite.color = new Color(fadeSprite.color.r,
				                             fadeSprite.color.g,
				                             fadeSprite.color.b,
				                             1f - (timeElapsed/fadeInTime));
				
				timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			
			fadeSprite.color = new Color(fadeSprite.color.r,
			                             fadeSprite.color.g,
			                             fadeSprite.color.b,
			                             0f);
		}


		private void Update()
		{
			if(Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.P))
			{
				KillPlayer();
			}
		}
	}
}
