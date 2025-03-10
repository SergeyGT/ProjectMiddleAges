using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private void OnEnable()
    {
        XPBar.MaxLevel += BossSpawn;
    }

    private void OnDisable()
    {
        XPBar.MaxLevel -= BossSpawn;
    }

    private void BossSpawn()
    {
        //Запуск анимации входа 

    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void FallDrop()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        
    }
}
