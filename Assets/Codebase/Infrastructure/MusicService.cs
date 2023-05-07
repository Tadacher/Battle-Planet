using System;
using UnityEngine;
using Zenject;

public class MusicService : MonoBehaviour
{
    MusicSetup _sfxSetup;
    AudioSource  _musicSource;
    MusicPlayer _musicPlayer;

    [Inject]
    private void Construct(MusicSetup sfx)
    {
        Debug.LogError("no sources injected");
        _sfxSetup = sfx;
        _musicPlayer = new MusicPlayer(_sfxSetup, _musicSource);
    }
    private void Update()
    {
        if (_musicPlayer.MusicPlaying()) _musicPlayer.StartNewTrack();
    }
}
