using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] musicClips;
    [SerializeField]
    AudioSource source;
    private void Update()
    {
        if (!source.isPlaying) StartNewTrack();
    }

    public void StartNewTrack()
    {
        source.clip = musicClips[Random.Range(0, musicClips.Length)];
        source.Play();
    }
}
