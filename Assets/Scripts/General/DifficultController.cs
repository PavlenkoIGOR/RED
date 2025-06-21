using UnityEngine;

public class DifficultController : MonoBehaviour 
{
    public int scores;

    public void AddScore(int scoreToAdd)
    {
        scores += scoreToAdd;
    }
}
