using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//Finite-State Machine Manager
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameState _currentState;
    private GameState _previousState;

    [Header("Screens")]
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _resultsScreen;
    [SerializeField] private GameObject _levelUpScreen;

    [Header("Current Stats Display")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentLevelDisplay;
    public TextMeshProUGUI currentWeaponsDisplay;

    [Header("Results Display")]
    public TextMeshProUGUI levelReachedDisplay;
    public TextMeshProUGUI timeSurvivedDisplay;
    public List<Image> chosenWeaponsDisplay = new List<Image>(); //Длина списка - макс кол-во оружия у персонажа

    [Header("Stopwatch")]
    private float _stopwatchTime;
    public TextMeshProUGUI stopwatchTimeDisplay;

    [Header("Audio In Game")]
    [SerializeField] private List<AudioClip> _audioClipList;

    public bool IsGameOver { get; private set; }
    public bool IsGamePaused { get; private set; }

    public bool isChoosingUpgrade {  get; private set; }
    //Состояния игры
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        { 
            Debug.LogWarning("Extra " + this + "deleted");
            Destroy(this);
        }

        DisableScreens();
    }

    private void Start()
    {
        //Пока без переключения треков
        SoundManager.Instance.PlayMusic(_audioClipList[0]);
    }

    private void Update()
    {
        switch(_currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatch();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!IsGameOver)
                {
                    IsGameOver=true;
                    Time.timeScale = 0f; 
                    Debug.Log("Game Over");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if (!isChoosingUpgrade)
                {
                    isChoosingUpgrade=true;
                    Time.timeScale = 0f;
                    Debug.Log("Upgrade state");
                    _levelUpScreen.SetActive(true);
                }
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
            IsGamePaused = true;
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
            IsGamePaused=false;
            Debug.Log("Игра вышла из паузы");
        }
    }


    private void DisableScreens()
    {
        _pauseScreen.SetActive(false);
        _resultsScreen.SetActive(false);
        _levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwatchTimeDisplay.text;
        ChangeState(GameState.GameOver);
    }

    private void DisplayResults()
    {
        _resultsScreen.SetActive(true);
    }

    public void AssignLevelReachedUI(int levelReached)
    {
        levelReachedDisplay.text = levelReached.ToString(); 
    }

    public void AssignChosenWeapons(List<Image> chosenWeaponsData)
    {
        if (chosenWeaponsDisplay.Count==chosenWeaponsData.Count)
        {
            chosenWeaponsDisplay = chosenWeaponsData;
        }
    }

    public void DisplayAssignedWeapon()
    {
        ///////[TODO]  получать список выбранного оружия от игрока
    }

    private void UpdateStopwatch()
    {
        _stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();
    }


    private void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(_stopwatchTime/60);
        int seconds = Mathf.FloorToInt(_stopwatchTime%60);

        stopwatchTimeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
    }

    public void EndLevelUp()
    {
        isChoosingUpgrade = false;
        Time.timeScale = 1.0f;
        _levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
