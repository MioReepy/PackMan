using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] _ghostObjects;
    [SerializeField] private PacMan _pacManObject;
    [SerializeField] private Transform _pelletTransform;
    public int Score { get; private set; }
    public int Lives { get; private set; }

    private float _delayTime = 2f;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.anyKey && Lives <= 0)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(Score);
        SetLives(3);
        NewRound();
    }

    private void GameOver()
    {
        foreach (var ghost in _ghostObjects)
        {
            ghost.gameObject.SetActive(false);
        }
        
        _pacManObject.gameObject.SetActive(false);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score =+ ghost.ScorePoint);
    }    
    
    public void PacManEaten()
    {
        _pacManObject.gameObject.SetActive(false);
        
        SetLives(Lives =- 1);

        if (Lives <= 0)
        {
            Invoke(nameof(ReserStart), _delayTime);
        }
        else
        {
            GameOver(); 
        }
    }

    private void ReserStart()
    {
        foreach (var ghost in _ghostObjects)
        {
            ghost.gameObject.SetActive(true);
        }
        
        _pacManObject.gameObject.SetActive(true);
    }

    private void NewRound()
    {
        foreach (Transform pellet in _pelletTransform)
        {
            pellet.gameObject.SetActive(true);
        }   
        
        ReserStart();
    }

    private void SetScore(int score)
    {
        Score = score;
    }    
    
    private void SetLives(int lives)
    {
        Lives = lives;
    }
}
