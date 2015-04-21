using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace WLB
{
	public class LedgeCheck : MonoBehaviour 
	{
		public LedgeGrabMotor ledgeGrabMotor;
		public WallRunMotor wallRunMotor;

		private List<GameObject> ledgeCheckObjects = new List<GameObject>(0);
		/*private void OnTriggerEnter2D()
		{
			ledgeGrabMotor.isLedge = true;
			wallRunMotor.isLedge = true;
		}
		
		
		private void OnTriggerExit2D()
		{
			ledgeGrabMotor.isLedge = false;
			wallRunMotor.isLedge = false;
		}*/

		private void OnTriggerEnter2D(Collider2D other)
		{
			ledgeGrabMotor.isLedge = true;
			wallRunMotor.isLedge = true;
			
			Debug.Log ("what the fuck");
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			for( int i = 0; i < ledgeCheckObjects.Count; i++)
			{
				if(ledgeCheckObjects[i] == other.gameObject)
				{
					ledgeCheckObjects.RemoveAt(i);
					break;
				}
			}
			
			if(ledgeCheckObjects.Count == 0)
			{
				ledgeGrabMotor.isLedge = false;
				wallRunMotor.isLedge = false;
			}
		}
	}
}