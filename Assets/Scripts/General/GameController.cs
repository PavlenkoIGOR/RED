using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    [SerializeField] private Button _startBttn;
    [SerializeField] private Button _restartBttn;
    [SerializeField] private Gun _playerGun;
    [SerializeField] private GameObject _joystick;
    //[SerializeField] private SpawnPoint _spawnPoint;

    private Vector3 _startPos;

    public DifficultController _difficultController;

    #region gameOver
    [SerializeField] private GameObject _gameOverScreen;
    [HideInInspector] public UnityEvent OnHeroDeath;
    #endregion

    [SerializeField] private ScoresTMP _scoresTMP;
    int tmpScores = default;

    #region timer
    float _timer_tmp = 0;
    [SerializeField] float _spawnTime = 2.0f;
    #endregion
    private void Start()
    {
        _startPos = _playerGun.transform.root.position;
        _gameState = GameState.MainMenu;
        _playerGun.enabled = false;
        _menuPanel.SetActive(true);
        _joystick.SetActive(false);
        _restartBttn.gameObject.SetActive(false);
        OnHeroDeath?.AddListener(GameOverAnim);
    }

    private protected void Update()
    {

        if (EnemySpawner.enemyesAlive.Count <= 0 && _isGameStarted)
        {
            if (Player.instance.score - tmpScores >= 100)
            {
                spawner.SpawnBoss();
                tmpScores = Player.instance.score;
                _difficultController.level++;
                _difficultController.OnLevelChange.Invoke();
            }
            else
            {
                spawner.SpawnRandomEnemyes();
            }
        }
    }

    public void StartGame()
    {
        EnemySpawner.enemyesAlive.Clear();

        _menuPanel.SetActive(false);
        _isGameStarted = true;
        _playerGun.enabled = true;
        spawner.SpawnRandomEnemyes();
        _joystick.SetActive(true);
        Time.timeScale = 1.0f;
        _isPause = false;
        Player.instance.ResetScores();  
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
            _startBttn.gameObject.SetActive(false);
            _restartBttn?.gameObject.SetActive(true);
        }
        _isPause = !_isPause;
    }

    public void Restart()
    {
        foreach (var spawnPoint in spawner.SpawnPoints)
        {
            spawnPoint.StopAllCoroutines();
        }

        if (EnemySpawner.enemyesAlive.Count > 0)
        {
            foreach (var enemy in EnemySpawner.enemyesAlive)
            {
                Destroy(enemy.gameObject);
            }
            EnemySpawner.enemyesAlive.Clear();
        }
        StartGame();

        Projectile[] projectiles = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }
        _playerGun.transform.root.position = _startPos;
    }

    private void GameOverAnim()
    {
        _ui.SetActive(false);
        _gameOverScreen.GetComponent<Animator>().Play("GameOverAnim");
        _gameOverScreen.GetComponent<AudioSource>().Play();
        _isGameStarted = false;
        EnemySpawner.enemyesAlive.Clear();
        OnHeroDeath.RemoveAllListeners();
    }
}
