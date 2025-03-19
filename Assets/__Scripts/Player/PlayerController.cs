using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _activeShootAim;
    [SerializeField] public float _speed = 12f;

    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private GameObject _pricel;

    [SerializeField] private AudioClip _step;

    private Rigidbody _rb;
    private Camera _cam;
    private Animator _animator;
    private AudioSource _source;
    private Vector3 _mousePoint;
    private Vector3 _movementVector;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _cam = Camera.main;
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Gameplay)
        {
            InputLogic();
            Aim();
            MoveLogic();
        }

        if (_movementVector != Vector3.zero)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);

            if (!_source.isPlaying)
            {
                SoundManager.Instance.PlayLocalSound(_source, _step);
            }
        }
        else
        {
            StopAudioPlaying(_step);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }
    }

    private void InputLogic()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _movementVector = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
    }

    private void Aim()
    {
        if (GetMousePosition())
        {
            var direction = _mousePoint - transform.position;
            direction.y = 0;
            _activeShootAim.forward = direction;
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
        if (_movementVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            Vector3 moveDelta = _movementVector * _speed * Time.deltaTime;
            _rb.MovePosition(transform.position + moveDelta);
        }
    }

    private void StopAudioPlaying(AudioClip clip)
    {
        if (_source.isPlaying && _source.clip == clip)
        {
            _source.Stop();
        }
    }
}
