﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
public class PlayerController : MonoBehaviour
{
    #region PlayerController
    [Header("Player")]
    public float MoveSpeed = 2.0f;
    public float BackSpeed = -2.0f;
    public float SprintSpeed = 5.335f;
    public float Gravity = 9.81f;
    public Vector3 _moveDirection; 
    [Header("Interacting")]
    public PlayerState state;

    [Header("Pickup Position")]
    public bool pickupOnHand;
    public Transform posHand;

    [Header("Mouse Setting")]
    [Range(0.0f, 2f)]
    public float sensitivity = 0.5f;

    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    public float SpeedChangeRate = 10.0f;

    [Header("Cinemachine")]
    public bool useCamera;
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _animationBlendX;
    private float _animationBlendY;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;


    // animation IDs
    private int _animIDSpeed;

    private int _animIDMotionSpeed;


    private PlayerInput _playerInput;

    public Animator _animator;
    [HideInInspector] public CharacterController _controller;
    [HideInInspector] public SoundManager _soundManager;
    [SerializeField] public InputManager _input;
    private GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private bool _hasAnimator;

    private bool IsCurrentDeviceMouse
    {
        get
        {

            return _playerInput.currentControlScheme == "KeyboardMouse";

        }
    }
    #endregion
    public Transform boxPos;
    public Transform robotPos;
    public bool canMove;
    private void Awake()
    {

        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        state  = GetComponent<PlayerState>();
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputManager>();
        _playerInput = GetComponent<PlayerInput>();
        _soundManager = FindAnyObjectByType<SoundManager>();


        AssignAnimationIDs();
        state.isInteractingBox = true;
    }

    private void Update()
    {
        sensitivity = _soundManager._mouse.value;
        if (!canMove)
        {
            Move();
        }
        else
        {          
            _animator.SetFloat("SpeedY", 0);
            _animator.SetFloat("SpeedX", 0);
            return;
        }

    }

    private void LateUpdate()
    {
        if (useCamera)
        {
            CameraRotation();
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("SpeedY");

        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }



    private void CameraRotation()
    {

        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {

            float deltaTimeMultiplier = IsCurrentDeviceMouse ? sensitivity : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }


        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);


        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void Move()
    {
        float Horizontal = _input.move.x;
        float Vertical = _input.move.y;

        float targetSpeed = MoveSpeed; // Default to MoveSpeed

        if (state.isInteractingBox)
        {
            targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed; // Update targetSpeed based on sprint input
           
        }
        else if (!state.isInteractingBox && Vertical < 0)  // ถ้าถอยหลัง
        {
            targetSpeed = BackSpeed;
        }
           

        if (!_controller.isGrounded)
        {
            _verticalVelocity -= Gravity * Time.deltaTime;
        }
        else
        {

            _verticalVelocity = -0.5f;
        }

        if (_input.move == Vector2.zero) targetSpeed = 0.0f;


        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;


        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {

            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        if (_input.move != Vector2.zero)
        {
            
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            if (state.isInteractingBox)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                             _mainCamera.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            else
            {
                
                _targetRotation = Mathf.Atan2(0f, _input.move.y) * Mathf.Rad2Deg + transform.eulerAngles.y;
            }

        }

        
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _moveDirection = new Vector3(_input.move.x, 0f, _input.move.y) * targetSpeed;
        float targetBlendX = _moveDirection.x;
        float targetBlendY = _moveDirection.z;

        _animationBlendX = Mathf.Lerp(_animationBlendX, targetBlendX, Time.deltaTime * SpeedChangeRate);
        _animationBlendY = Mathf.Lerp(_animationBlendY, targetBlendY, Time.deltaTime * SpeedChangeRate);

        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        _animator.SetFloat("SpeedY", _animationBlendY);
        _animator.SetFloat("SpeedX", _animationBlendX);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
    }


    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void SignalCanMoveEnabled()
    {
        canMove = true;
    }

    public void SignalCanMoveDisabled()
    {
        canMove = false;
    }

    public void StateCamera(bool newState)
    {
        GameManager.Instance.InteractUI = newState;
    }


    #region save

    public void Save()
    {
        PlayerData playerData = new PlayerData
        {
            position = transform.position,
            rotation = transform.rotation
        };

        string dataPath = Application.persistentDataPath + "/PlayerData.json";
        string jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(dataPath, jsonData);
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath + "/PlayerData.json";

        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);

            transform.position = playerData.position;
            transform.rotation = playerData.rotation;
        }
    }

    #endregion
}


[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;
}

