using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float _speed = 0.3f;

    private Rigidbody _rb;

    public Vector3 LastMovedVector {  get; private set; }
    public Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        LastMovedVector = Vector3.right;
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

        MovementVector = new Vector3(horizontalInput, 0.0f, verticalInput);

        SetLastMovedVector();
    }


    private void SetLastMovedVector()
    {
        if (MovementVector.x!=0 || MovementVector.z!=0)
        {
            LastMovedVector = new Vector3(MovementVector.x, 0, MovementVector.z);
        }
    }


    private void MoveLogic()
    {
        _rb.AddForce(MovementVector * _speed);
        transform.rotation = Quaternion.LookRotation(LastMovedVector);
    }


}