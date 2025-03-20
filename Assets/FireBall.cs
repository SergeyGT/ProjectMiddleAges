using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(1,0,0) * 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
