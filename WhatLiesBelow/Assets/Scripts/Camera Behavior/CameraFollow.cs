using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public UnityEngine.Camera camera;
	public GameObject player;
	private float followx;
	private float followy;
	private float lastx;
	private float lasty;
	
	// Update is called once per frame
	void Update () {
		followx = player.transform.position.x;
		followy = player.transform.position.y;
	}

	void CameraGo(){
		Debug.Log ("Camera Follow");
		//camera.transform.position.x = followx;
		//camera.transform.position.y = followy;
		camera.transform.Translate (followx, followy, 0);
	}

	void CameraStop(){
		Debug.Log ("Camera Stop");
		lastx = player.transform.position.x;
		lasty = player.transform.position.y;
		//camera.transform.position.x = lastx;
		//camera.transform.position.y = lasty;
		camera.transform.Translate (lastx, lasty, 0);
		}


}
