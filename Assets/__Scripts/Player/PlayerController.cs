using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Tooltip("Маска для всего, во что может целиться игрок")]
    [SerializeField] private LayerMask _groundMask;

    [Tooltip("Задает transform.forward контроллеру активки")]
    [SerializeField] private Transform _activeShootAim;

    [SerializeField] public float _speed = 12f;

    [SerializeField] private AudioClip _step;

    private ParticleSystem _partilceDust;
    private AudioSource _source;


    private Rigidbody _rb;
    private Camera _cam;
    private Animator _animator;
    private Vector3 _mousePoint;
    public Vector3 LastRotationVector { get; private set; }
    public Vector3 MovementVector { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
        //_partilceDust = GetComponentInChildren<ParticleSystem>();

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _cam = Camera.main;

        LastRotationVector = transform.forward;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.Gameplay)
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
            StopAudioPLaying(_step);
            //_partilceDust.Stop();
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }
    }


    private void InputLogic()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MovementVector = new Vector3(horizontalInput, 0.0f, verticalInput);

        LastRotationVector = transform.rotation.eulerAngles;
    }

    private void Aim()
    {
        if (GetMousePosition())
        {
            var direction = _mousePoint - transform.position;

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
        if (MovementVector != Vector3.zero)
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);

            if (!_source.isPlaying)
            {
                SoundManager.Instance.PlayLocalSound(_source, _step);
            }

            // Поворот в направление движения
            Quaternion targetRotation = Quaternion.LookRotation(MovementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);

            // Движение
            Vector3 moveDelta = MovementVector.normalized * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + moveDelta);
        }
        else
        {
            StopAudioPLaying(_step);
            //_partilceDust.Stop();
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }
    }

    private void StopAudioPLaying(AudioClip clip)
    {
        if (_source.isPlaying && _source.clip == clip)
        {
            _source.Stop();
        }
    }
}