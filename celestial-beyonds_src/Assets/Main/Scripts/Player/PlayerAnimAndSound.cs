using System.Collections;
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
    private int _profile;

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
    }

    private void Update()
    {
        _animator.SetFloat(_profile, _rb.velocity.magnitude / maxSpeed);
    }
}