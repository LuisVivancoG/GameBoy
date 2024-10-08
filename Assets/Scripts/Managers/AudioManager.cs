using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundSO[] _sounds;
    public static AudioManager _instance;

    private void Awake()
    {
        foreach (SoundSO s in _sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.spatialBlend = s.SpatialBlend;
            s.Source.priority = s.Priority;
            s.Source.playOnAwake = s.PlayOnAwake;
        }
    }

    public void Play(string name)
    {
        SoundSO s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Missing sound '" + name + "' in the AudioManager array");
            return;
        }
        s.Source.Play();
    }
}
