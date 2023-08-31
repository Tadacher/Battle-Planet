using Infrastructure;
using Services;
using UnityEngine;

public class MusicService
{
    private AudioSource _source;
    private MusicPlayerService _musicPlayer;
    public MusicService(MusicSetup musicSetup, TickableService service, LocationInstaller locationInstaller)
    {
        
        GameObject protosource = new GameObject("sfxSource");
        protosource.AddComponent<AudioSource>();
        protosource.transform.parent = locationInstaller.transform;
        _source = protosource.GetComponent<AudioSource>();

        _musicPlayer = new MusicPlayerService(musicSetup, _source);
        service.tickDelegate += Tick;

    }

    public void Tick()
    {
        if (!_musicPlayer.MusicPlaying()) 
            _musicPlayer.StartNewTrack();
    }
}
