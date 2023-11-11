using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum SoundType { Music, SFX };

[Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3)]
    public float minPitch = 1;
    [Range(.1f, 3)]
    public float maxPitch = 1;

    public bool loop;

    public SoundType type;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    public float mainVolume = 1;
    public float musicVolume = 1;
    public float effectsVolume = 1;

    protected override void Awake()
    {
        base.Awake();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;

        sound.source.pitch = UnityEngine.Random.Range(sound.minPitch, sound.maxPitch);
        if (sound.loop)
        {
            if (sound.source.isPlaying) return;
            sound.source.Play();
        }
        else
            sound.source.PlayOneShot(sound.clip);
    }

    public void Stop(string name)
    {
        Sound sound = GetSound(name);
        sound.source.Stop();
    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            try
            {
                Debug.LogWarning("Sound: " + sound.name + " null!");
            }
            catch
            {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            return null;
        }
        return sound;
    }

    public float getVolume(SoundType soundType)
    {
        float volume = mainVolume;
        switch (soundType)
        {
            case SoundType.Music:
                volume *= musicVolume;
                break;
            case SoundType.SFX:
                volume *= effectsVolume;
                break;
        }
        return volume;
    }

    public void UpdateVolumes()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = getVolume(sound.type);
        }
    }
}
