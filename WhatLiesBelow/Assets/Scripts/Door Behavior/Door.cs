using UnityEngine;
using System.Collections;

namespace WLB
{
	public class Door : MonoBehaviour 
	{
		public bool sceneTransition = false;
		public Door exit;
		public string scene;

		public GameObject player;
		public float coolDown = 3f;

		public bool canTravel = true;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(canTravel)
			{
				canTravel = false;
				StartCoroutine(Cooldown());
				if(sceneTransition)
				{
					Application.LoadLevel(scene);
				}
				else
				{
					exit.canTravel = false;
					StartCoroutine(exit.Cooldown());
					player.transform.position = exit.transform.position;
				}
			}
		}

		public IEnumerator Cooldown()
		{
			yield return new WaitForSeconds (coolDown);
			canTravel = true;
		}
	}
}