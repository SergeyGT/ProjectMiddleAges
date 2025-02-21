using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _fillHp;
    private Player _player;


    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        if (_player != null)
        {
            _player.OnHealthChanged += UpdateUiHp; 
        }
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnHealthChanged -= UpdateUiHp; 
        }
    }

    private void UpdateUiHp(int currentHp, int maxHp)
    {
        _fillHp.fillAmount = (float)currentHp / maxHp;
    }



}
