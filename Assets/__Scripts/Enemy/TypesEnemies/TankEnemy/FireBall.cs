using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileWeaponBehaviour
{

    private void OnEnable()
    {
        transform.position += new Vector3(0, 1, 0);
    }

    private void Update()
    {
        transform.position += transform.forward * weaponData.Speed * Time.deltaTime;
    }
    private new void OnTriggerEnter(Collider other) // Собственная реализация метода 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage((int)weaponData.Damage);
        }
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
