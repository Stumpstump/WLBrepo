using UnityEngine;
using System.Collections;


namespace WLB
{
	public class MossModule : MonoBehaviour 
	{
		public int mossCount = 3;
		public int maxMoss = 5;
		public Transform lightPosition;
		public GameObject mossLightPrefab;

		public int startMossCount = 3;

		private GameObject heldLight;
		private bool holdingLight = false;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Q) && mossCount > 0 && holdingLight == false)
			{
				if(heldLight) Destroy(heldLight);
				mossCount -= 1;
				GameObject light = Instantiate(mossLightPrefab, lightPosition.position, Quaternion.identity) as GameObject;
				holdingLight = true;
				heldLight = light;
				heldLight.GetComponentInChildren<Collider2D>().enabled = false;
			}
			else if (Input.GetKeyDown(KeyCode.Q) && holdingLight == true)
			{
				heldLight.GetComponentInChildren<Collider2D>().enabled = true;
				holdingLight = false;
			}

			if(holdingLight)
			{
				heldLight.transform.position = lightPosition.position;
			}
		}
	}
}
