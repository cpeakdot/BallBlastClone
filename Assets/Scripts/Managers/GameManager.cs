using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameState gameState;
    public static event Action OnGameEnded;

    private void Awake() 
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

    public void EndGame()
    {
        gameState = GameState.Ended;

        OnGameEnded?.Invoke();
    }
}
