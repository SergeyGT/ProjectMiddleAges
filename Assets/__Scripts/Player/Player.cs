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
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 2f, transform.forward, out hit))
        {
            IInteract interactable = hit.collider.gameObject.GetComponent<IInteract>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
