using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private static AudioManager instance = null;
	public static AudioManager Instance {
			get { return instance; }
		}
		
		
		void Awake() {
			if (instance != null && instance != this)
			{
				audio.Stop();
				if(instance.audio.clip != audio.clip)
				{
					instance.audio.clip = audio.clip;
					instance.audio.volume = audio.volume;
					instance.audio.Play();
				}
				Destroy(this.gameObject);
				return;
			}
			instance = this;
			audio.Play ();
			DontDestroyOnLoad(this.gameObject);
		}
	}