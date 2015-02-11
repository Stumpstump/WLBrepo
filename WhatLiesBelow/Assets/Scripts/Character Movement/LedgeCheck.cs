using UnityEngine;
using System.Collections;


namespace WLB
{
	public class LedgeCheck : MonoBehaviour 
	{
		public LedgeGrabMotor ledgeGrabMotor;


		private void OnTriggerEnter2D()
		{
			ledgeGrabMotor.isLedge = true;
		}
		
		
		private void OnTriggerExit2D()
		{
			ledgeGrabMotor.isLedge = false;
		}
	}
}