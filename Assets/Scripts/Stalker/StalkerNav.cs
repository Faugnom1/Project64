using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class StalkerNav : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingDistanceThreshold;
    [SerializeField] private UnityEvent _onDestinationReached;

    [SerializeField] private float _maxSpeedTime;
    [SerializeField] private float _minSpeedTime;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;

    [SerializeField] private AudioClip _roarClip;
    [SerializeField] private AudioClip[] _stepClips;

    private NavMeshAgent _agent;

    private bool _inScriptedEvent;
    private bool _isChasing;
    private float _speedTime;
    private float _roarTimer;
    private float _stepsTimer;

    private void Awake()
    {
        _roarTimer = Random.Range(4f, 6f);
        _stepsTimer = 1f;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (_inScriptedEvent)
        {
            CheckDestinationReached();
        }
        else if (_isChasing)
        {
            HandleChasing();
            RoarAtRandomInterval();
            PlayStepSoundEffectAtInterval();
        }
    }

    private void HandleChasing()
    {
        if (_speedTime >= 0)
        {
            _agent.speed = _maxSpeed;
            _agent.SetDestination(_target.position);
        }
        else if (_speedTime < 0)
        {
            _agent.speed = _minSpeed;
        }
        if (_speedTime < -_minSpeedTime)
        {
            _speedTime = _maxSpeedTime;
        }
        _speedTime -= Time.deltaTime;
    }

    private void RoarAtRandomInterval()
    {
        _roarTimer -= Time.deltaTime;

        if (_roarTimer <= 0)
        {
            SoundEffectsManager.Instance.PlaySoundEffect(_roarClip, (Vector2)transform.position);
            _roarTimer = Random.Range(4f, 6f);
        }
    }

    private void PlayStepSoundEffectAtInterval()
    {
        _stepsTimer -= Time.deltaTime;

        if (_stepsTimer <= 0)
        {
            int clipIndex = Random.Range(0, _stepClips.Length - 1);
            SoundEffectsManager.Instance.PlaySoundEffect(_stepClips[clipIndex], (Vector2)transform.position);
            _stepsTimer = 1f;
        }
    }

    public void SetScriptedEventDestination(Vector2 position, float speed)
    {
        _agent.isStopped = false;
        _inScriptedEvent = true;
        _agent.speed = speed;
        _agent.SetDestination(new Vector3(position.x, position.y, 0));
    }

    public void CheckDestinationReached()
    {
        Debug.Log(_agent.speed);
        // Check if the agent has a valid path and its remaining distance is less than the threshold
        if (!_agent.pathPending && _agent.remainingDistance <= _stoppingDistanceThreshold && !_agent.hasPath)
        {
            _inScriptedEvent = false;
            _onDestinationReached.Invoke();
        }
        Debug.Log(_agent.destination);
    }

    public void ChasePlayer()
    {
        _isChasing = true;
    }

    public void StopChase()
    {
        _isChasing = false;
    }

    public void SnapPosition(Vector2 position)
    {
        _agent.enabled = false;
        transform.position = new Vector3(position.x, position.y, 0);
        _agent.enabled = true;
    }
}
