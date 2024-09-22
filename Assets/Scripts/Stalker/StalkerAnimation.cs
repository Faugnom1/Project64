using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StalkerAnimation : MonoBehaviour
{
    [SerializeField] private bool _walkLeft;
    [SerializeField] private bool _walkRight;
    [SerializeField] private bool _walkDown;
    [SerializeField] private bool _walkUp;

    private Animator _animator;

    private void Awake()
    {
        _walkLeft = false;
        _walkRight = false;
        _walkDown = false;
        _walkUp = false;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_walkLeft)
        {
            StartLeftAnimation();
        }
        else if (_walkRight)
        {
            StartRightAnimation();
        }
        else if (_walkUp)
        {
            StartUpAnimation();
        }
        else if (_walkDown)
        {
            StartDownAnimation();
        }
        else
        {
            StartIdleAnimation();
        }
    }

    public void StartRightAnimation()
    {
        _walkLeft = false;
        _walkDown = false;
        _walkUp = false;

        _animator.SetTrigger("WalkHorizontal");
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void StartLeftAnimation()
    {
        _walkRight = false;
        _walkDown = false;
        _walkUp = false;

        _animator.SetTrigger("WalkHorizontal");
        transform.localScale = new Vector3(-1, 1, 1);
    }

    public void StartDownAnimation()
    {
        _walkRight = false;
        _walkLeft = false;
        _walkUp = false;

        _animator.SetTrigger("WalkDown");
    }

    public void StartUpAnimation()
    {
        _walkRight = false;
        _walkLeft = false;
        _walkDown = false;

        _animator.SetTrigger("WalkUp");
    }

    public void StartIdleAnimation()
    {
        _walkRight = false;
        _walkLeft = false;
        _walkDown = false;
        _walkUp = false;

        _animator.SetTrigger("Idle");
    }
}
