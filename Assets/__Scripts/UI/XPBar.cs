using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;
using System;
using UnityEngine.InputSystem;

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
    private Diamonds _diamond;
    private List<Diamonds> diamondsList = new List<Diamonds>();

    public static Action MaxLevel;

    private void Start()
    {
        currentLevel = Level.L.numL;
        previousLevelXP = (int)curve.Evaluate(currentLevel);
        nextLevelXP = (int)curve.Evaluate(currentLevel + 1);
    }


    private void OnEnable()
    {
        Diamonds.OnDiamondSpawned += SubscribeToDiamonds;
    }

    private void OnDisable()
    {
        Diamonds.OnDiamondSpawned -= SubscribeToDiamonds;

        // Отписываемся от всех оставшихся алмазов
        foreach (var diamond in diamondsList)
        {
            if (diamond != null)
            {
                diamond.XpChanged -= AddExp;
            }
        }
        diamondsList.Clear();
    }

    private void SubscribeToDiamonds(Diamonds diamond)
    {
        if (!diamondsList.Contains(diamond))
        {
            diamondsList.Add(diamond);
            diamond.XpChanged += AddExp;
        }
    }

    private void AddExp(int amount)
    {
        print(amount);
        totalXP += amount;
        print(totalXP);
        

        CheckForLevelUp();
        UpdateInterface();
    }
    private void CheckForLevelUp()
    {
        print("Check level");
        if (levelText != null)
        {
            if(totalXP >= nextLevelXP)
            {
                if (Level.L.numL == 50)
                {
                    MaxLevel?.Invoke();
                }
                else
                {
                    Level.L.numL++;
                    currentLevel++;
                    UpdateLevel();
                }
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


    private void Update()
    {
        if (Keyboard.current.ctrlKey.isPressed && Keyboard.current.lKey.wasPressedThisFrame)
        {
            Level.L.numL++;
            currentLevel++;
            UpdateLevel();
            UpdateInterface();
        }
    }
}
