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
    private Animator _animator;
    private Vector3 _mousePoint;
    public Vector3 LastRotationVector {  get; private set; }
    public Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _cam = Camera.main;

        LastRotationVector = transform.forward;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameOver && !GameManager.Instance.IsGamePaused)
        {
            InputLogic();
            Aim();
        }
    }
    private void FixedUpdate()
    {
        if (MovementVector != Vector3.zero) MoveLogic();
        else
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }
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

            LastRotationVector = transform.forward;
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
        _animator.SetBool("Idle", false);
        _animator.SetBool("Walk", true);
        _rb.AddForce(MovementVector * _speed);
    }


}