using UnityEngine;
public enum GameState
{
    MainMenu,
    Game,
    Pause
}

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;

    private bool _isGameStart;
    private bool _isPause;
    private GameState _gameState;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Gun _playerGun;
    [SerializeField] private GameObject _joystick;

    //private ScoreController _scoreController;
    [SerializeField] private ScoresTMP _scoresTMP;

    //private LevelController levelController;

    private void Start()
    {
        _gameState = GameState.MainMenu;
        _playerGun.enabled = false;
        _menuPanel.SetActive(true);
        _joystick.SetActive(false);
    }

    private protected void Update()
    {
        if (_gameState == GameState.MainMenu)
        {

        }

        if (_gameState == GameState.Game)
        {

        }

        if (_gameState == GameState.Pause)
        {
        }

        int tmpScores = default;
        if (Player.instance.score != tmpScores)
        {
            _scoresTMP._onScoreChanges?.Invoke();
            tmpScores = Player.instance.score;
        }

    }

    public void StartGame()
    {
        _menuPanel.SetActive(false);
        _isGameStart = true;
        _playerGun.enabled = true;
        spawner.SpawnRandomEnemyes();
        _joystick.SetActive(true);
    }

    public void PauseGame()
    {
        if (_isPause)
        {
            Time.timeScale = 1.0f;
            _menuPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            _menuPanel.SetActive(true);
        }
        _isPause = !_isPause;
    }
}
