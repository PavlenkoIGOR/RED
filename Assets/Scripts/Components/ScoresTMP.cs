using TMPro;
using UnityEngine;

public class ScoresTMP : MonoBehaviour
{
    [SerializeField] private TMP_Text _scores;
    public TMP_Text scores { get => _scores; }

    private void Update()
    {
        _scores.text = "Scores: " + Player.instance.score.ToString();
    }
}
