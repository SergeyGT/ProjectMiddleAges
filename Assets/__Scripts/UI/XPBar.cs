using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;

public class XPBar : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve curve;

    private int currentLevel;
    private int totalXP;
    private int previousLevelXP;
    private int nextLevelXP;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image fill;

    public static XPBar Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("XPBar уже существует, уничтожаем дубликат.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentLevel = Level.L.numL;
        previousLevelXP = (int)curve.Evaluate(currentLevel);
        nextLevelXP = (int)curve.Evaluate(currentLevel + 1);
    }

    public void AddExp(int amount)
    {
        totalXP += amount;
        CheckForLevelUp();
        UpdateInterface();
    }
    private void CheckForLevelUp()
    {
        if (levelText != null)
        {
            if(totalXP >= nextLevelXP)
            {
                Level.L.numL++;
                currentLevel++;
                UpdateLevel();
            }
        }
    }

    private void UpdateLevel()
    {
        previousLevelXP = (int)curve.Evaluate(currentLevel);
        nextLevelXP = (int)curve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        int start = totalXP - previousLevelXP;
        int end = nextLevelXP - previousLevelXP;
        levelText.text = currentLevel.ToString();
        fill.fillAmount = (float)start/(float)end;
    }
}
