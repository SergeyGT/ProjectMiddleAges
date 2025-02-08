using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 10;

    private void FixedUpdate()
    {
        MovementLogic();
    }


    private void MovementLogic()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3 (horInput, 0.0f, vertInput);

        transform.Translate(moveDirection * _speed * Time.fixedDeltaTime);
    }
}
