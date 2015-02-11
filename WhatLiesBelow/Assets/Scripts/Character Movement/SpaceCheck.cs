using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class SpaceCheck : MonoBehaviour 
	{
		public LedgeGrabMotor ledgeGrabMotor;

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log ("wtf");
			List<GameObject> gos = ledgeGrabMotor.gosInSpaceCheck;

			if (gos.Count == 0)
			{
				gos.Add(other.gameObject);
				ledgeGrabMotor.gosInSpaceCheck = gos;
			} 
			else 
			{
				foreach(GameObject go in gos)
				{
					if(go == other.gameObject)
					{
						break;
					}
					else
					{
						gos.Add(go);
						ledgeGrabMotor.gosInSpaceCheck = gos;
					}
				}
			}

			Debug.Log (gos.Count);
			ledgeGrabMotor.spaceEmpty = gos.Count == 0;
		}


		private void OnTriggerExit2D(Collider2D other)
		{
			List<GameObject> gos = ledgeGrabMotor.gosInSpaceCheck;

			foreach(GameObject go in gos)
			{
				if(go == other.gameObject)
				{
					gos.Remove(go);
					break;
				}
			}

			ledgeGrabMotor.spaceEmpty = gos.Count == 0;
		}
	}
}