using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hp;

    public void TakeDamage(int damage)
    {
        _hp = Mathf.Max(0, _hp-damage);
        if(_hp == 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        CheckDrop();
    }

    private void CheckDrop()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach(Collider collider in colliders)
        {
            IInteract interact = collider.gameObject.GetComponent<IInteract>();
            if(interact != null)
            {
                interact.Interact();
            }
        }
    }
}
