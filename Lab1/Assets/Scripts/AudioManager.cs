using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

	private static AudioManager m_Instance;

	public Sound[] sounds;

	//public Text toggleMusicText;

	//public Text toggleSFXText;

	void Awake()
	{
		if (m_Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();

			s.source.clip = s.clip;
			s.source.loop = s.loop;

		}
		setLoop("Theme");
		Play("Theme");
		//Play("Explosion");
	}

	private void Start()
	{
		//toggleMusicText.text = "OFF";
		//toggleSFXText.text = "OFF";
	}

	private void Update()
	{
	}

	/// <summary>
	/// Use to play an audio file
	/// </summary>
	/// <param name="sound">The name of the file you want to play</param>
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Play();
	}

	/// <summary>
	/// Used to stop an audio manually through code
	/// </summary>
	/// <param name="name">The file name you want to stop</param>
	public static void Stop(string name)
	{
		Sound s = Array.Find(m_Instance.sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found! Can't stop it!");
			return;
		}
		s.source.Stop();
	}

	/// <summary>
	/// Used to change the volume of Music files through options
	/// </summary>
	public void SetMusicVolume(float t_volume)
	{
		foreach (Sound s in sounds)
		{
			s.source.volume = t_volume;
		}
	}

	/// <summary>
	/// Used to change the volume of sfx files through options
	/// </summary>
	public void SetSFXVolume(float t_volume)
	{
		foreach (Sound s in sounds)
		{
			s.source.volume = t_volume;
		}
	}


	public float GetCurrentMusicVolume()
	{
		foreach (Sound s in sounds)
		{
			return s.source.volume;
		}
		return 0;
	}

	public float GetCurrentSFXVolume()
	{
		foreach (Sound s in sounds)
		{
			return s.source.volume;
		}
		return 0;
	}

	public void setLoop(string name)
    {
		//foreach (Sound s in sounds)
  //      {
		//	if (s.source.name == name && s.source.loop == true)
  //          {
		//		s.source.loop = false;
  //          }
		//	else if (s.source.name == name && s.source.loop == false)
		//	{
		//		s.source.loop = true;
		//	}
		//}
    }
}
