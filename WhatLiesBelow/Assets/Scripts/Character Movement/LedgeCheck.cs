using UnityEngine;
using System.Collections;


namespace WLB
{
	public class LedgeCheck : MonoBehaviour 
	{
		public LedgeGrabMotor ledgeGrabMotor;
		public WallRunMotor wallRunMotor;

		private void OnTriggerEnter2D()
		{
			ledgeGrabMotor.isLedge = true;
			wallRunMotor.isLedge = true;
		}
		
		
		private void OnTriggerExit2D()
		{
			ledgeGrabMotor.isLedge = false;
			wallRunMotor.isLedge = false;
		}
	}
}