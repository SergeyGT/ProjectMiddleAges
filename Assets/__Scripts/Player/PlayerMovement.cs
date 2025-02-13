using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float _speed = 0.3f;

    private Rigidbody _rb;

    ///////////////////////
    private float lastHor;
    private float lastVert;
    public Vector3 lastMovedVector;
    public Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        lastMovedVector = Vector3.right;
    }

    private void Update()
    {
        InputLogic();
    }
    private void FixedUpdate()
    {
        MoveLogic();
    }


    private void InputLogic()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MovementVector = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        SetLastMovedVector();
    }


    private void SetLastMovedVector()
    {
        if (MovementVector.x!=0)
        {
            lastHor= MovementVector.x;
            lastMovedVector = new Vector3(lastHor, 0, 0);
        }

        if (MovementVector.z!=0)
        {
            lastVert= MovementVector.z;
            lastMovedVector = new Vector3(0,0, lastVert);
        }

        if (MovementVector.x!=0 && MovementVector.z!=0)
        {
            lastMovedVector = new Vector3(lastHor, 0, lastVert);
        }
    }


    private void MoveLogic()
    {
        _rb.AddForce(MovementVector * _speed);
    }


}