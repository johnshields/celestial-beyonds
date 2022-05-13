using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * PlayerAnimAndSound
 * Script that controls the Player's animations & sounds.
 */
public class PlayerAnimAndSound : MonoBehaviour
{
    public float maxSpeed = 5f;
    private Animator _animator;
    private Rigidbody _rb;
    private InputProfiler _controls;
    private GameObject _player;
    private int _profile, _jumpActive;
    private bool _action;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = new InputProfiler();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _profile = Animator.StringToHash("Profile");
        _jumpActive = Animator.StringToHash("JumpActive");
    }

    private void Update()
    {
        if(!_action)
            _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
    }
    
    private void OnEnable()
    {
        _controls.Profiler.Jump.started += Jump;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.Jump.started -= Jump;
        _controls.Profiler.Disable();
    }
    
    private void Jump(InputAction.CallbackContext obj)
    {
        _action = true;
        _animator.SetFloat(_profile, 5);
        _animator.SetTrigger(_jumpActive);
        StartCoroutine(ActionOver());
    }

    IEnumerator ActionOver()
    {
        yield return new WaitForSeconds(.6f);
        _action = false;
    }
}