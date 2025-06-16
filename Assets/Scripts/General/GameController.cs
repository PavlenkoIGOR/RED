using UnityEngine;
using UnityEngine.Events;
public enum GameState
{
    MainMenu,
    Game,
    Pause
}

public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject _ui;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private BaffSpawner _baffSpawner;

    private bool _isGameStarted;
    private bool _isPause;
    private GameState _gameState;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Gun _playerGun;
    [SerializeField] private GameObject _joystick;

    #region gameOver
    [SerializeField]private GameObject _gameOverScreen;
    [HideInInspector]public UnityEvent OnHeroDeath;
    #endregion

    [SerializeField] private ScoresTMP _scoresTMP;
    int tmpScores = default;

    #region timer
    float _timer_tmp = 0;
    [SerializeField] float _spawnTime = 2.0f;
    #endregion
    private void Start()
    {
        _gameState = GameState.MainMenu;
        _playerGun.enabled = false;
        _menuPanel.SetActive(true);
        _joystick.SetActive(false);

        OnHeroDeath?.AddListener(GameOverAnim);
    }

    private protected void Update()
    {

        if (EnemySpawner.enemyesAlive.Count<=0 && _isGameStarted)
        {
            if (Player.instance.score - tmpScores >= 100)
            {

            }
            spawner.SpawnRandomEnemyes();
        }
    }

    public void StartGame()
    {
        _menuPanel.SetActive(false);
        _isGameStarted = true;
        _playerGun.enabled = true;
        spawner.SpawnRandomEnemyes();
        _joystick.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        if (_isPause)
        {
            Time.timeScale = 1.0f;
            _menuPanel.SetActive(false);
            _joystick.SetActive(true);
        }
        else
        {
            Time.timeScale = 0.0f;
            _menuPanel.SetActive(true);
            _joystick.SetActive(false);
        }
        _isPause = !_isPause;
    }

    private void GameOverAnim()
    {
        _ui.SetActive(false);
        _gameOverScreen.GetComponent<Animator>().Play("GameOverAnim");
        OnHeroDeath.RemoveAllListeners();
    }
}
