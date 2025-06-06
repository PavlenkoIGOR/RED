using Unity.VisualScripting;
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
    private GameState _gameState;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Gun _playerGun;

    private void Start()
    {
        _gameState = GameState.MainMenu;
        _playerGun.enabled = false;
        _menuPanel.SetActive(true);
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
    }

    public void StartGame()
    {
        _menuPanel.SetActive(false);
        _isGameStart = true;
        _playerGun.enabled = true;
    }
}
