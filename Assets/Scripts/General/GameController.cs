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
    [SerializeField] private GameObject _heroPref;
    [SerializeField] private DifficultController _difficultController;
    [SerializeField] private GameObject _ui;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BaffSpawner _baffSpawner;

    private bool _isGameStarted;
    private bool _isPause;
    private GameState _gameState;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _startBttn;
    [SerializeField] private Button _restartBttn;
    [SerializeField] private GameObject _joystick;

    private Vector3 _startPos;

    #region gameOver
    [SerializeField] private GameObject _gameOverScreen;
    [HideInInspector] public static UnityEvent OnHeroDeath = new UnityEvent();
    #endregion

    [SerializeField] private ScoresTMP _scoresTMP;
    int tmpScores = default;

    #region timer
    float _timer_tmp = 0;
    [SerializeField] float _spawnTime = 2.0f;
    #endregion


    private int _deviceRes_X;
    private int _deviceRes_Y;
    // ѕолучение границ видимой области камеры
    float screenLeft;
    float screenRight;
    float screenBottom;
    float screenTop;

    [HideInInspector]public GameObject hero;

    private void Awake()
    {
        _deviceRes_X = Screen.width;
        _deviceRes_Y = Screen.height;

        // ѕолучение границ видимой области камеры
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;
        _gameOverScreen.SetActive(false);
    }
    private void Start()
    {
        _startPos = new Vector2((screenRight + screenLeft )/ 2, (screenTop+ screenBottom) / 2);
        _gameState = GameState.MainMenu;

        hero = Instantiate(_heroPref, _startPos, Quaternion.identity);

        foreach (Gun gun in hero.GetComponent<Hero>().guns)
        {
            gun.enabled = false;
        }
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
                _enemySpawner.SpawnBoss();
                tmpScores = Player.instance.score;
            }
            else
            {
                _enemySpawner.SpawnRandomEnemyes();
            }
        }
    }

    public void StartGame()
    {
        EnemySpawner.enemyesAlive.Clear();

        _menuPanel.SetActive(false);
        _isGameStarted = true;


        foreach (var gun in hero.GetComponent<Hero>().guns)
        {
            gun.enabled = true;
        }



        _enemySpawner.SpawnRandomEnemyes();
        _joystick.SetActive(true);
        Time.timeScale = 1.0f;
        _isPause = false;
        Player.instance.ResetScores();
        _baffSpawner.canSpawnShield = true;
        _baffSpawner.canSpawnRocket = true;
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
        foreach (var spawnPoint in _enemySpawner.SpawnPoints)
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
        

        Projectile[] projectiles = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }

        hero = Instantiate(_heroPref, _startPos, Quaternion.identity);

        StartGame();
        _gameOverScreen.SetActive(false);
    }

    private void GameOverAnim()
    {
        _ui.SetActive(false);
        _gameOverScreen.SetActive(true);
        _isGameStarted = false;
        _baffSpawner.canSpawnShield = false;
        _baffSpawner.canSpawnRocket = false;
        _menuPanel.SetActive(true);
        _startBttn.gameObject.SetActive(false);
        _restartBttn?.gameObject.SetActive(true);
    }
}
