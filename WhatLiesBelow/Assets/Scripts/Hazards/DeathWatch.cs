using UnityEngine;
using System.Collections;

namespace WLB
{
	public class DeathWatch : MonoBehaviour {

		public static DeathModule _deathModule;

		public DeathModule deathModule;
		
		
		private void Awake()
		{
			_deathModule = deathModule;
		}
	}
}
