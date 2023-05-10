using Infrastructure;
using System;
using UnityEngine;
using Zenject;

public class MusicService
{
    MusicSetup _musicSetup;
    AudioSource _source;
    MusicPlayer _musicPlayer;
    TickDelegate tickDelegate;
    public MusicService()
    {
    }
    
    [Inject]
    public void Init(MusicSetup musicSetup, TickableService service, LocationInstaller locationInstaller)
    {
        Debug.Log("injection");
        _musicSetup = musicSetup;
        
        GameObject protosource = new GameObject("sfxSource");
        protosource.AddComponent<AudioSource>();
        protosource.transform.parent = locationInstaller.transform;
        _source = protosource.GetComponent<AudioSource>();

        _musicPlayer = new MusicPlayer(musicSetup, _source);
        tickDelegate += Tick;
        service.tickDelegate += Tick;
        Debug.Log("injection2");

    }

    public void Tick()
    {
        Debug.Log("music tick");

        if (!_musicPlayer.MusicPlaying()) _musicPlayer.StartNewTrack();
    }
}
