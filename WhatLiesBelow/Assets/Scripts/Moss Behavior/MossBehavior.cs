using UnityEngine;
using System.Collections;

namespace WLB
{
	public class MossBehavior : MonoBehaviour {

		public Light light;
		public float dimTime = 15f;
		public float startIntensity = 1f;

		private void Start()
		{
			StartCoroutine(DimLight());
		}


		private void DestroyGameobject()
		{
			Destroy(gameObject);
		}


		private IEnumerator DimLight()
		{
			float timeElapsed = 0.0f;
			float currentIntensity = startIntensity;

			while(timeElapsed < dimTime)
			{
				currentIntensity = startIntensity - (timeElapsed / dimTime);

				light.intensity = currentIntensity;
				timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			light.intensity = 0.0f;
			DestroyGameobject();
		}
	}
}
