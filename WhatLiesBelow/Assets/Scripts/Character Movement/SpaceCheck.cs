using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class SpaceCheck : MonoBehaviour 
	{
		public LedgeGrabMotor ledgeGrabMotor;
		public WallRunMotor wallRunMotor;

		private void OnTriggerEnter2D(Collider2D other)
		{
			List<GameObject> gos = ledgeGrabMotor.gosInSpaceCheck;

			if (gos.Count == 0)
			{
				gos.Add(other.gameObject);
				ledgeGrabMotor.gosInSpaceCheck = gos;
			} 
			else
			{
				for(int i = gos.Count - 1; i >= 0; i--)
				{
					if(gos[i] == other.gameObject)
					{
						break;
					}
					else
					{
						gos.Add(gos[i]);
						ledgeGrabMotor.gosInSpaceCheck = gos;
					}
				}
			}

			if(gos.Count == 0)
			{
				ledgeGrabMotor.spaceEmpty = true;
				wallRunMotor.spaceEmpty = true;
			}
			else
			{
				ledgeGrabMotor.spaceEmpty = false;
				wallRunMotor.spaceEmpty = false;
			}
		}


		private void OnTriggerExit2D(Collider2D other)
		{
			List<GameObject> gos = ledgeGrabMotor.gosInSpaceCheck;

			for(int i = gos.Count - 1; i >= 0; i--)
			{
				if(gos[i] == other.gameObject)
				{
					gos.Remove(gos[i]);
					break;
				}
			}

			if(gos.Count == 0)
			{
				ledgeGrabMotor.spaceEmpty = true;
				wallRunMotor.spaceEmpty = true;
			}
			else
			{
				ledgeGrabMotor.spaceEmpty = false;
				wallRunMotor.spaceEmpty = false;
			}
		}
	}
}