using UnityEngine;

public class SfxService : MonoBehaviour
{
    [SerializeField] SfxSetup _sfxSetup;

    [SerializeField] AudioSource 
        _musicSource,
        _effectsSource;

    MusicPlayerService _musicService;
    SoundEffectsService _soundEffectsService;
    private void Awake()
    {
        _musicService = new MusicPlayerService(_sfxSetup, _musicSource);
        _soundEffectsService = new SoundEffectsService( _effectsSource, _sfxSetup);
    }

    private void Update()
    {
        if (_musicService.MusicPlaying()) _musicService.StartNewTrack();
    }
}
