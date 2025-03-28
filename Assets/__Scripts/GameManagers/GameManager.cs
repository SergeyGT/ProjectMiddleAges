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
    [SerializeField] private GameObject _gameplayScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _levelUpScreen;
    [SerializeField] private GameObject _resultsScreen;

    [Header("Current Stats Display")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentLevelDisplay;
    public TextMeshProUGUI currentWeaponsDisplay;

    [Header("Results Display")]
    public TextMeshProUGUI levelReachedDisplay;
    public TextMeshProUGUI timeSurvivedDisplay;
    public List<Image> chosenWeaponsDisplay = new List<Image>(); //����� ������ - ���� ���-�� ������ � ���������

    [Header("Stopwatch")]
    private float _stopwatchTime;
    public TextMeshProUGUI stopwatchTimeDisplay;

    [Header("Audio In Game")]
    [SerializeField] private List<AudioClip> _audioClipList;


    [SerializeField] private GameObject _player;
    
    private Player _playerScript;

    public bool IsGameOver { get; private set; }
    public bool IsGamePaused { get; private set; }
    public bool isChoosingUpgrade {  get; private set; }


    private int _gameLevel = 0;


    //��������� ����
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
        //���� ��� ������������ ������
        SoundManager.Instance.PlayMusic(_audioClipList[0]);
        _playerScript = _player.GetComponent<Player>();
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
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                LevelUp();
                break;
            default:
                Debug.LogWarning("������������ ������� ���������! " + _currentState);
                break;
        }
    }


    public GameState GetCurrentGameState()
    {
        return _currentState;
    }


    private void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState==GameState.Paused)
            {
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
            Time.timeScale = 0f; //��������� ����
            IsGamePaused = true;
            _pauseScreen.SetActive(true);
            Debug.Log("���� ������ �� �����");
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
            Time.timeScale = 1f; //������������� ����
            _pauseScreen.SetActive(false);
            IsGamePaused=false;
            Debug.Log("���� ����� �� �����");
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
        _gameplayScreen.SetActive(false);
        _resultsScreen.SetActive(true);
    }

    private void LevelUp()
    {
        if (_gameLevel == 20)
        {
            GameOver();
        }


        //[ToDo] ������ �������� � �������� ���� ��� ������
        _playerScript.StartParticles();

        if (_playerScript._particleSystem.isPlaying)
        {
            if (!isChoosingUpgrade)
            {
                isChoosingUpgrade = true;
                Time.timeScale = 0f;
                _gameLevel++;
                _levelUpScreen.SetActive(true);
            }
        }
        
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
        ///////[TODO]  �������� ������ ���������� ������ �� ������
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
        _player.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        isChoosingUpgrade = false;
        Time.timeScale = 1.0f;
        _levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }

    public void AssignChosenWeaponsToUI(List<Image> chosenWeapons)
    {

        if (chosenWeaponsDisplay.Count != chosenWeapons.Count)
        {
            Debug.Log("UI weapon list is not equal to paramter list");
            return;
        }

        for (int i = 0; i < chosenWeaponsDisplay.Count; i++)
        {
            if (chosenWeapons[i].sprite)
            {
                chosenWeaponsDisplay[i].enabled = true;
                chosenWeaponsDisplay[i].sprite = chosenWeapons[i].sprite;
            }
            else //optional
            {
                chosenWeaponsDisplay[i].enabled = false;    
            }
        }

    }
}
