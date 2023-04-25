using UnityEngine;

public class MusicPlayerService 
{

    SfxSetup _sfxSetup;
    AudioSource _source;

    public MusicPlayerService(SfxSetup sfxSetup, AudioSource source)
    {
       _sfxSetup = sfxSetup;
        _source = source;
    }

    public void StartNewTrack()
    {
        _source.clip = _sfxSetup._musicClips[Random.Range(0, _sfxSetup._musicClips.Length)];
        _source.Play();
    }

    public bool MusicPlaying() => _source.isPlaying;
}
