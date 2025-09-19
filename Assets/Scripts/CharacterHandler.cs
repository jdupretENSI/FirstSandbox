using System;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{

    public Vector2 Move;
    
    [SerializeField] private Vector3 _input;
    [SerializeField] private Vector3 _targetVelocity;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _smoothSpeed = 0.5f;
    [SerializeField] private float _maxVelocity = 0.01f; 
    private float _acceleration;
    
    private Animator _animator;
    private bool _jump;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Inputs
        _input.z = Input.GetAxis("Vertical");
        _input.x = Input.GetAxis("Horizontal");
        _targetVelocity = Vector3.SmoothDamp(_input, _targetVelocity, ref _velocity, _smoothSpeed);
        
        // Movement
        transform.Translate(Vector3.ClampMagnitude(_targetVelocity, _maxVelocity));
        
        
        // Animation
        _animator.SetFloat("vertical", _targetVelocity.z);
        _animator.SetFloat("horizontal", _targetVelocity.x);
        // _animator.SetBool("jump", Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("jumping");
        }
    }
}
