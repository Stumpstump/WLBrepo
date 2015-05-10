using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class LevelLoadManager : MonoBehaviour 
	{

		public static List<Vector3> entryPoints = new List<Vector3> ();

		public List<Vector3> defaultEntryPoints = new List<Vector3> ();

		public GameObject _playerPrefab;

		public static GameObject playerPrefab;
		public static GameObject player;
		public static int level = 0;

		public static void SetEntryPoint(Vector3 entryPoint)
		{
			Debug.Log (entryPoint);
			entryPoints[level] = entryPoint;
			return;

		}

		public static Vector3 GetEntryPoint()
		{
			if(entryPoints[level] != null)
				return entryPoints[level];

			Debug.Log ("entry point not found!");
			return Vector3.zero;
		}
		
		private void OnLevelWasLoaded(int lvl) 
		{
			//record the level we just loaded
			level = lvl;

			//put the player in the scene wherever they should be spawning
			player = Instantiate (playerPrefab, entryPoints [lvl], Quaternion.identity) as GameObject;
			DeathWatch._deathModule = player.GetComponentInChildren<DeathModule> () as DeathModule;

			//set spawnpoint to wherever they entered the level by default
			Transform trans = new GameObject ().transform;
			trans.position = entryPoints [lvl];
			DeathWatch._deathModule.spawnPoint = trans;
		}
		
		private void Awake()
		{
			playerPrefab = _playerPrefab;
			entryPoints = defaultEntryPoints;
			DontDestroyOnLoad(transform.gameObject);
		}
	}
}

