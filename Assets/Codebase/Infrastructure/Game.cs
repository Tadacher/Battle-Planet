using Zenject;
using Codebase.Infrastructure;

namespace Infrastructure
{
    public class Game
    {
        PlayerInput _playerInput;
        SfxService _sfxService;
        MusicService _musicService;
        CoroutineProcessor _coroutineProcessor;
        EnemySpawner _enemySpawner;
        UiService _uiService;
        ScoreControll _scoreControll;

        [Inject]
        public void Init(PlayerInput playerInput, SfxService sfxService, MusicService musicService, CoroutineProcessor coroutineProcessor, EnemySpawner enemySpawner, UiService uiService, ScoreControll scoreControll)
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