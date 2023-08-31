using UnityEngine;

namespace Services
{
    public class MusicPlayerService
    {

        MusicSetup _sfxSetup;
        AudioSource _source;

        public MusicPlayerService(MusicSetup sfxSetup, AudioSource source)
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
}