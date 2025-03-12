using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Finite-State Machine Manager
public class GameManager : MonoBehaviour
{

    [SerializeField] private GameState _currentState;
    private GameState _previousState;

    [Header("UI")]
    [SerializeField]private GameObject _pauseScreen;

    //Состояния игры
    private enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    private void Awake()
    {
        DisableScreens();
    }

    private void Update()
    {
        switch(_currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                ///
                break;
            default:
                Debug.LogWarning("Недопустимое игровое состояние! " + _currentState);
                break;
        }
    }

    private void CheckForPauseAndResume()
    {
        Debug.Log("Check");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pressed");
            if (_currentState==GameState.Paused)
            {
                Debug.Log("Resume");
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (_currentState!=GameState.Paused)
        {
            _previousState = _currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //Остановка игры
            _pauseScreen.SetActive(true);
            Debug.Log("Игра встала на паузу");
        }
    }

    private void ChangeState(GameState state)
    {
        _currentState = state;
    }

    public void ResumeGame()
    {
        if (_currentState == GameState.Paused)
        {
            ChangeState(_previousState);
            Time.timeScale = 1f; //Возобновление игры
            _pauseScreen.SetActive(false);
            Debug.Log("Игра вышла из паузы");
        }
    }


    private void DisableScreens()
    {
        _pauseScreen.SetActive(false);
    }

}
