using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoresTMP : MonoBehaviour
{
    [SerializeField] private TMP_Text _scores;
    public UnityEvent _onScoreChanges;
    private void Awake()
    {
        _onScoreChanges.AddListener(ChangeScore);
    }
    //private void Update()
    //{
    //    int tmpScores = default;
    //    if (Player.instance.score != tmpScores)
    //    {
    //        _onScoreChanges.Invoke();
    //        tmpScores = Player.instance.score;
    //    }
    //}
    void ChangeScore()
    {
        _scores.text = "Scores: " + Player.instance.score.ToString();
    }
}
