using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveProjectileBehaviour : ProjectileWeaponBehaviour
{
    private void OnEnable()
    {
        //”станавливаю горизонтальное положение стрелы
        Vector3 currentRotation = transform.rotation.eulerAngles;

        currentRotation.x -= 90; 

        transform.rotation = Quaternion.Euler(currentRotation);
    }


    protected void Update()
    {
        transform.position -= transform.up * Time.deltaTime * weaponData.Speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}

