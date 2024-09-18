using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private bool _isSirenOn;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Material _sirenOnMaterial;
    [SerializeField] private Material _sirenOffMaterial;
    [SerializeField] private GameObject _leftSpotLight;
    [SerializeField] private GameObject _rightSpotLight;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _previousSirenState;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_isSirenOn)
        {
            ToggleSiren(true);
        }
    }

    private void Update()
    {
        if (_isSirenOn != _previousSirenState)
        {
            ToggleSiren(_isSirenOn);
            _previousSirenState = _isSirenOn;
        }

        // Turn the spotlights
        if (_isSirenOn)
        {
            _leftSpotLight.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
            _rightSpotLight.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }

    public void ToggleSiren(bool isActive)
    {
        _isSirenOn = isActive;

        _animator.SetBool("SirenOn", isActive);
        _leftSpotLight.SetActive(isActive);
        _rightSpotLight.SetActive(isActive);
        _spriteRenderer.material = isActive ? _sirenOnMaterial : _sirenOffMaterial;
    }
}
