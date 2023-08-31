
using Services;

namespace Infrastructure
{
    public class Game
    {
        PlayerInputService _playerInput;
        SfxService _sfxService;
        MusicService _musicService;
        CoroutineProcessorService _coroutineProcessor;
        EnemySpawnerService _enemySpawner;
        UiService _uiService;
        ScoreService _scoreControll;

        
        public Game (PlayerInputService playerInput, SfxService sfxService, MusicService musicService, CoroutineProcessorService coroutineProcessor, EnemySpawnerService enemySpawner, UiService uiService, ScoreService scoreControll)
        {
            _playerInput = playerInput;
            _sfxService = sfxService;
            _musicService = musicService;
            _coroutineProcessor = coroutineProcessor;
            _enemySpawner = enemySpawner;
            _uiService = uiService;
            _scoreControll = scoreControll;
        }
    }
} 