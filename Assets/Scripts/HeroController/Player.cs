using UnityEngine;

public class Player : SingletonBase<Player>
{


    [SerializeField] private int _livesQuantity;


    private int _score;
    private int _numKills;

    public int score => _score;
    public int numKills => _numKills;
    public int numLives => _livesQuantity;

    public bool hasShield = false;
    public bool hasRocket = false;

    void Start()
    {
        Respawn();
    }

    void Update()
    {

    }

    private void OnShipDeath()
    {
        _livesQuantity--;

        if (_livesQuantity > 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {

    }

    public void AddScore(int num)
    {
        _score += num;
    }

    public void AddKill()
    {
        _numKills++;
    }
    public void ResetScores()
    {
        _score = 0;
    }
}

