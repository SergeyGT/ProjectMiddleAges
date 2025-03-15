using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    public float fireRate = 0.2f;
    public float phaseBreakDuration = 3f;
    public int projectileCount = 30;
    public float waveAmplitude = 2f;
    public float waveFrequency = 2f;
    public int meleeDamage = 20;

    private bool isAttackin = false;


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
        print("Spawned Boss");
        // Запуск анимации входа 
        

        // Запускаем первую фазу
        StartCoroutine(PhaseOne());
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void FallDrop()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator PhaseOne()
    {
        isAttackin = true;

        for (int i = 0; i < projectileCount; i++)
        {
            ShootProjectile(i);
            yield return new WaitForSeconds(fireRate);
        }

        yield return new WaitForSeconds(phaseBreakDuration);

        // Переход к ближней атаке
        StartCoroutine(MeleeAttack());
    }

    private void ShootProjectile(int index)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

       
        float x = Mathf.Sin(index * waveFrequency) * waveAmplitude;
        Vector3 direction = new Vector3(x, 0, -1).normalized;

        rb.velocity = direction * projectileSpeed;
    }

    private IEnumerator MeleeAttack()
    {
        OnDrawGizmosSelected();
        

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<Player>().TakeDamage(meleeDamage);
            }
        }

        yield return new WaitForSeconds(2f);

        // Запускаем снова первую фазу
        StartCoroutine(PhaseOne());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
