using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void Start()
    {
        transform.position = new Vector3(0, 1, 0);
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0,0,1) * 2f * Time.fixedDeltaTime  );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
