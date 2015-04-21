using UnityEngine;
using System.Collections;

public class BlinkMotor : MonoBehaviour {

	public float maxBlinkTime = 5f;
	public float minBlinkTime = 0.3f;

	private void Start()
	{
		StartCoroutine (Blink ());
	}


	private IEnumerator Blink()
	{
		float blinkTime = Random.Range (minBlinkTime, maxBlinkTime);
		float elapsedTime = 0.0f;

		while(elapsedTime < blinkTime)
		{
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		StartCoroutine (Blink ());
	}
}
