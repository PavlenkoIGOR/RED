using UnityEngine;

public class ScoreController : MonoBehaviour 
{
    public int scores;

    public void AddScore(int scoreToAdd)
    {
        scores += scoreToAdd;
    }
}
