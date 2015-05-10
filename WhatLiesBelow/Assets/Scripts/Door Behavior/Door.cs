using UnityEngine;
using System.Collections;

namespace WLB
{
	public class Door : MonoBehaviour 
	{
		public Transform exit;
		public string scene;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!string.IsNullOrEmpty(scene))
			{
				LevelLoadManager.SetEntryPoint(exit.transform.position);
				Application.LoadLevel(scene);
			}
		}
	}
}