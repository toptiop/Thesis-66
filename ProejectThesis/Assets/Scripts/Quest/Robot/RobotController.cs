﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    #region PlayerController
    [Header("Player")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;
    public float Gravity = 9.81f;

    [Header("FLY Setting")]
    public bool useFlying;
    public bool isFly;
    public bool canFly;
    public float upSpeed = 1;
    public float downSpeed = 1;
    public float speedSmoothTime = 1;
    public Vector3 currentVelocity = Vector3.zero;

    public float smoothTime = 1;

    [Header("Grounded Setting")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float heightDistance = 1.5f;
    public float lowDistance = 1.5f;
    public Vector3 GroundedRadius;
    public LayerMask GroundLayers;

    [Header("Interacting")]
    public RobotState state;

    [Header("Mouse Sensitivity")]
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
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    public Vector3 velocity;

    // animation IDs
    private int _animIDSpeed;

    private int _animIDMotionSpeed;


    private PlayerInput _playerInput;

    public Animator _animator;
    [HideInInspector] public CharacterController _controller;
    private InputRobotManager _input;
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

        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputRobotManager>();
        _playerInput = GetComponent<PlayerInput>();


        AssignAnimationIDs();
        state.isInteractingBox = true;
    }

    private void Update()
    {

        if (!canMove)
        {
            Move();
           
            CheckGround();
            GroundedCheck();
        }
        else
        {
            _animator.SetFloat(_animIDSpeed, 0);
        }


    }

    private void LateUpdate()
    {
        if (useCamera)
        {
            CameraRotation();
        }

        if (useFlying)
        {
            Flying();
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");

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

        float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

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

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            if (state.isInteractingBox)
            {
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        if (_animator != null)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    void Flying()
    {
        Vector3 movement = Vector3.zero;

        if (_input.jump)
        {
            movement += Vector3.up * upSpeed;
        }
        else if (_input.down && !Grounded && canFly)
        {
            movement += Vector3.down * downSpeed;
        }

        Vector3 targetPosition = transform.position + movement;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, speedSmoothTime);

        transform.position = smoothedPosition;
    }

    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider)
            {
                Debug.Log(hit.distance);
                if (hit.distance >= heightDistance)
                {
                    canFly = true;
                }
                else if (hit.distance < lowDistance)
                {
                    isFly = false;
                    canFly = false;

                    float distanceToMove = lowDistance - hit.distance; // คำนวณหาระยะที่ต้องเคลื่อนที่เพิ่มขึ้น
                    Vector3 targetPosition = transform.position + Vector3.up * distanceToMove;
                    Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // ทำการเคลื่อนที่อย่างนุ่มนวล
                    transform.position = smoothedPosition;


                }

                /* if (!isFly && hit.distance >= 1.5)
                  {
                      // คำนวณ gravity
                      _verticalVelocity -= Gravity * Time.deltaTime;
                  }*/

                if (hit.distance > 1.7)
                {
                    isFly = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawCube(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }
    void GroundedCheck() //เช็คพื้น
    {
        Vector3 boxCenter = transform.position - Vector3.up * GroundedOffset;
        Grounded = Physics.CheckBox(boxCenter, new Vector3(GroundedRadius.x, GroundedRadius.y, GroundedRadius.z), Quaternion.identity, GroundLayers, QueryTriggerInteraction.Ignore);
    }


}
