using Unity.VisualScripting;
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
    [SerializeField] private GameObject _leftFinishPos;
    [SerializeField] private GameObject _rightFinishPos;
    [SerializeField] private GameObject _menuCanvas;
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
    [SerializeField] private Button _pauseBttn;
    private Vector2 stickStartPos;

    private Vector3 _startPos;

    #region gameOver
    [SerializeField] private GameObject _gameOverScreen;
    [HideInInspector] public static UnityEvent OnHeroDeath = new UnityEvent();
    #endregion

    [SerializeField] private ScoresTMP _scoresTMP;
    int tmpScores = default;




    private int _deviceRes_X;
    private int _deviceRes_Y;
    // Получение границ видимой области камеры
    float screenLeft;
    float screenRight;
    float screenBottom;
    float screenTop;

    [HideInInspector] public GameObject hero;

    private void Awake()
    {


        _deviceRes_X = Screen.width;
        _deviceRes_Y = Screen.height;
        _ui.GetComponent<CanvasScaler>().referenceResolution = new Vector2(_deviceRes_X, _deviceRes_Y);
        _menuCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(_deviceRes_X, _deviceRes_Y);

        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;


        _leftFinishPos.transform.position = new Vector2(screenLeft + 0.4f, _leftFinishPos.transform.position.y);
        _rightFinishPos.transform.position = new Vector2(screenRight - 0.4f, _rightFinishPos.transform.position.y);


        _gameOverScreen.SetActive(false);
        foreach (Transform uiChild in _ui.transform)
        {
            uiChild.gameObject.SetActive(false);
        }
        stickStartPos = _joystick.transform.Find("Stick").position;
    }
    private void Start()
    {
        _startPos = new Vector2((screenRight + screenLeft) / 2, (screenTop + screenBottom) / 2);
        _gameState = GameState.MainMenu;

        hero = Instantiate(_heroPref, _startPos, Quaternion.identity);

        foreach (Gun gun in hero.GetComponent<Hero>().guns)
        {
            gun.enabled = false;
        }
        _menuPanel.SetActive(true);
        _restartBttn.gameObject.SetActive(false);
        OnHeroDeath?.AddListener(GameOverAnim);
    }

    private bool isBossActive = false; // флаг, что босс активен
    private float spawnTimer = 0f;
    private protected void Update()
    {
        //_difficultController.CheckSpawnPosibility();
        print($"level {_difficultController.level}");


        if (_difficultController.level < 10 & _isGameStarted == true)
        {
            // Логика для уровней ниже 10
            if (EnemySpawner.enemyesAlive.Count <= 0 && _isGameStarted)
            {
                _difficultController.level++;
                if (Player.instance.score - tmpScores >= 500)
                {
                    _enemySpawner.SpawnBoss();
                    tmpScores = Player.instance.score;
                    isBossActive = true; // активируем босс
                }
                else
                {
                    _enemySpawner.SpawnRandomEnemyes(_difficultController.level);
                }
                
            }
        }
        else
        {
            // Уровень >= 10
            if (isBossActive)
            {
                // Ждем пока босс и все враги не убиты
                if (EnemySpawner.enemyesAlive.Count == 0)
                {
                    // Все враги и босс убиты — цикл можно начать заново
                    isBossActive = false;
                    spawnTimer = 0f;
                }
            }
            else
            {
                // Нет активного босса — проверяем условие для вызова босса
                if (Player.instance.score - tmpScores >= 500)
                {
                    // Перед вызовом босса убедимся, что все враги убиты
                    if (EnemySpawner.enemyesAlive.Count == 0)
                    {
                        _enemySpawner.SpawnBoss();
                        tmpScores = Player.instance.score;
                        isBossActive = true; // активируем босса
                    }
                    // Если враги еще есть — ждем их уничтожения
                }
                else
                {
                    // Спавним врагов через интервал _spawnTime
                    spawnTimer += Time.deltaTime;
                    if (spawnTimer >= _difficultController._spawnTime)
                    {
                        spawnTimer = 0f;
                        _enemySpawner.SpawnRandomEnemyes(_difficultController.level);
                    }
                }
            }
        }
    }

    public void StartGame()
    {
        _isGameStarted = true;


        _difficultController.level = 1;
        //_difficultController.CheckSpawnPosibility();
        EnemySpawner.enemyesAlive.Clear();

        _menuPanel.SetActive(false);


        foreach (var gun in hero.GetComponent<Hero>().guns)
        {
            gun.enabled = true;
        }

        foreach (Transform uiChild in _ui.transform)
        {
            //if (uiChild.GetComponent<VirtualJoystick>() is not VirtualJoystick)
            //{
                uiChild.gameObject.SetActive(true);
            //}            
        }

        _enemySpawner.SpawnRandomEnemyes(_difficultController.level);


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
            foreach (Transform uiChild in _ui.transform)
            {
                uiChild.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform uiChild in _ui.transform)
            {
                uiChild.gameObject.SetActive(false);
            }

            Time.timeScale = 0.0f;
            _menuPanel.SetActive(true);
            _scoresTMP.enabled = true;
            _startBttn.gameObject.SetActive(false);
            _restartBttn?.gameObject.SetActive(true);
        }
        _pauseBttn.gameObject.SetActive(true);
        _isPause = !_isPause;
    }

    public void Restart()
    {
        _difficultController.ResetParams();
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
        if (hero != null)
        {
            Destroy(hero);
        }
        hero = Instantiate(_heroPref, _startPos, Quaternion.identity);



        StartGame();
        _gameOverScreen.SetActive(false);
        _joystick.transform.Find("Stick").position = stickStartPos;
        VirtualJoystick.Value = Vector2.zero;


    }

    private void GameOverAnim()
    {
        foreach (Transform uiChild in _ui.transform)
        {
            uiChild.gameObject.SetActive(false);
        }
        _gameOverScreen.SetActive(true);
        _isGameStarted = false;
        _baffSpawner.canSpawnShield = false;
        _baffSpawner.canSpawnRocket = false;

        _menuPanel.SetActive(true);
        var saundBG = _menuPanel.transform.Find("Sound_BG");
        if (saundBG != null)
        {
            saundBG.gameObject.SetActive(false);
        }

        _startBttn.gameObject.SetActive(false);
        _restartBttn?.gameObject.SetActive(true);
    }
}
