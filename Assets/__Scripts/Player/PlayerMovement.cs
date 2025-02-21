using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [Tooltip("ћаска дл€ всего, во что может целитьс€ игрок")]
    [SerializeField] private LayerMask _groundMask;



    [SerializeField] public float _speed = 0.3f;

    private Rigidbody _rb;
    private Camera _cam;

    private Vector3 _mousePoint;
    public Vector3 LastMovedVector {  get; private set; }
    public Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _cam = Camera.main;

        LastMovedVector = transform.forward;
    }

    private void Update()
    {
        InputLogic();
        Aim();
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
    }

    private void Aim()
    {
        if (GetMousePosition())
        {
            var direction = _mousePoint - transform.position;

            direction.y = 0;

            transform.forward = direction.normalized;

            LastMovedVector = transform.forward;
        }
    }

    private bool GetMousePosition()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _groundMask))
        {
            _mousePoint = hitInfo.point;
            return true;
        }
        return false;
    }

    private void MoveLogic()
    {
        _rb.AddForce(MovementVector * _speed);
    }


}