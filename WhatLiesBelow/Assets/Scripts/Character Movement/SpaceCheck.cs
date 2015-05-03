using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WLB
{
	public class SpaceCheck : MonoBehaviour 
	{
		public List<GameObject> gos = new List<GameObject> ();

		private bool isEmpty;

		public bool IsEmpty
		{
			get
			{
				if(gos.Count == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (gos.Count == 0)
			{
				gos.Add(other.gameObject);
			} 
			else
			{
				bool add = true;
				for(int i = gos.Count - 1; i >= 0; i--)
				{
					if(gos[i] == other.gameObject)
					{
						add = false;
						break;
					}
				}

				if(add)
				{
					gos.Add(other.gameObject);
				}
			}
		}


		private void OnTriggerExit2D(Collider2D other)
		{
			for(int i = gos.Count - 1; i >= 0; i--)
			{
				if(gos[i] == other.gameObject)
				{
					gos.Remove(gos[i]);
				}
			}
		}
	}
}