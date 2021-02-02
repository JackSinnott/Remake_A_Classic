using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        play("Theme");
    }

    public void play(string t_name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == t_name);

        if (s.source.name == "Theme")
        {
            s.source.loop = true;
        }
        s.source.Play();
    }
}
