using UnityEngine;
using System.Collections;

namespace WLB
{
	public class DeathPit : MonoBehaviour 
	{

		public void OnTriggerEnter2D(Collider2D other)
		{
			if(other.tag == "Player")
			{
				DeathWatch._deathModule.KillPlayer();
			}
		}
	}
}
