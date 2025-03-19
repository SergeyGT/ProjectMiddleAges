using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Tooltip("Маска для всего, во что может целиться игрок")]
    [SerializeField] private LayerMask _groundMask;

    [Tooltip("Задает transform.forward контроллеру активки")]
    [SerializeField] private Transform _activeShootAim;

    [SerializeField] public float _speed = 0.3f;

    [SerializeField] private GameObject _pricel;

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
        _pricel.SetActive(true);

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

        MovementVector = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
    }

    private void Aim()
    {
        if (GetMousePosition())
        {
            var direction = _mousePoint - transform.position;

            _activeShootAim.forward = direction;


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
            _pricel.transform.position = _mousePoint;
            return true;
        }
        return false;
    }

    private void MoveLogic()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("Walk", true);
        if (!_source.isPlaying)
        {
            SoundManager.Instance.PlayLocalSound(_source, _step);
        }
        //_partilceDust.Play();
        _rb.AddForce(MovementVector * _speed);
    }

    private void StopAudioPLaying(AudioClip clip)
    {
        if (_source.isPlaying && _source.clip == clip)
        {
            _source.Stop();
        }
    }
}