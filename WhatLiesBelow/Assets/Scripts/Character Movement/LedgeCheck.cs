using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace WLB
{
	public class LedgeCheck : MonoBehaviour 
	{
		public List<GameObject> ledgeCheckObjects = new List<GameObject>(0);

		private bool isLedge;
		
		public bool IsLedge
		{
			get
			{
				if(ledgeCheckObjects.Count == 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (ledgeCheckObjects.Count == 0)
			{
				ledgeCheckObjects.Add(other.gameObject);
			} 
			else
			{
				for(int i = ledgeCheckObjects.Count - 1; i >= 0; i--)
				{
					if(ledgeCheckObjects[i] == other.gameObject)
					{
						break;
					}
					else
					{
						ledgeCheckObjects.Add(ledgeCheckObjects[i]);
					}
				}
			}
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
		}
	}
}