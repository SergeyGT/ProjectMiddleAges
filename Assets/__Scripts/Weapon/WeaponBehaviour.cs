using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Vector3 Direction { get; protected set; }


    [SerializeField] private float _destroyAfterSeconds;
    protected virtual void Start()
    {
        Destroy(gameObject, _destroyAfterSeconds);
        Direction = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.transform.GetComponent<Enemy>().Die....
        //Destroy(gameObject);
    }
}
