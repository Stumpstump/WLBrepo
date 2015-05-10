using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class Level : MonoBehaviour 
	{
		public List<Door> doorsInLevel = new List<Door> ();
		public List<Hazard> hazardsInLevel = new List<Hazard> ();

		public Door currentDoor;

		private void Start()
		{
			if(!PlayerPrefs.HasKey(EditorApplication.currentScene)) 
			{
				PlayerPrefs.SetString(EditorApplication.currentScene, currentDoor.GetInstanceID().ToString());
			}
		}
	}
}