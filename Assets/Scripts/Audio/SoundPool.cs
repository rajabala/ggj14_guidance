using UnityEngine;
using System.Collections;

public class SoundPool : MonoBehaviour {

	static SoundPool _instance;
	public AudioClip[] sounds;

	public AudioSource musicPlayer;
	
	//List<AudioSource> sources = new List<AudioSource>();
	// Use this for initialization
	void Awake () 
	{
		if (!_instance)
		{
			_instance = this;
		}
		DontDestroyOnLoad(this);
	}



	public static void playClip(int sound)
	{ 
		GameObject soundManager = GameObject.Find("soundManager");
		
		if (soundManager != null)//delete this crap later, this just stops errors from choking the consol
		{
			if (_instance.sounds != null)
			{
				AudioSource audioSource = _instance.gameObject.AddComponent<AudioSource>();
				audioSource.clip = _instance.sounds[sound];
				audioSource.Play();
				Destroy(audioSource, audioSource.clip.length + 1); //was 2.0f
			}

		}
	}


	
	//overload for looping sounds
	public static void playClip(int sound, bool loopingSound)
	{ 
		GameObject soundManager = GameObject.Find("soundManager");
		
		if (soundManager != null)//delete this crap later, this just stops errors from choking the consol
		{
			
			AudioSource audioSource = _instance.gameObject.AddComponent<AudioSource>();
			
			if (_instance.sounds != null && loopingSound == true)
			{
				
				audioSource.clip = _instance.sounds[sound];
				audioSource.Play();
				audioSource.loop = true;
			}
			else
			{
				Destroy(audioSource, audioSource.clip.length + 1);
			}
		}
	}
	
	public static bool soundIsPlaying(int sound)
	{
		int amountPlaying = 0;
		AudioSource[] sounds = _instance.GetComponents<AudioSource>();
		for (int i =0; i < sounds.Length; i++)
		{
			//for particular sound look for the matching clip
			if (sounds[i].clip == _instance.sounds[sound])
				amountPlaying++;
		}
		if (amountPlaying > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public static void clearLoopingSounds()
	{
		AudioSource[] sounds = _instance.GetComponents<AudioSource>();
		for (int i =0; i < sounds.Length; i++)
		{
			//for particular sound look for the matching clip
			if (sounds[i].loop == true)
				Destroy(sounds[i]);
		}
		
	}
	
	public static void stopClip(int sound)
	{
		AudioSource[] sounds = _instance.GetComponents<AudioSource>();
		for (int i =0; i < sounds.Length; i++)
		{
			//for particular sound look for the matching clip
			if (sounds[i].clip == _instance.sounds[sound])
				Destroy(sounds[i]);
		}
		
	}
	
	public static void playSong(int song)
	{
		GameObject soundManager = GameObject.Find("soundManager");
		
		if (soundManager != null)
		{
			
			if (_instance.sounds != null)
			{
				_instance.musicPlayer.clip = _instance.sounds[song];
				_instance.musicPlayer.Play();
				_instance.musicPlayer.loop = true;
			}
		}
	}
}
