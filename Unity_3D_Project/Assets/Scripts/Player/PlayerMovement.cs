using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 3f;

    [SerializeField]
    private float _runSpeed = 5f;

    [SerializeField]
    private float _applySpeed;

    // 스테미나
    [SerializeField]
    private float _stamina;
    private float _initstamina = 10f;
    public Slider StaminaSlider;


    [SerializeField]
    private float jumpForce;

    private bool isRun = false;
    public bool isMove = false;
    private bool isGround = true;

    // 민감도
    [SerializeField]
    private float lookSensitivity;

    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    // 필요한 컴포넌트
    private Camera _eye;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;
    private PlayerInput _input;
    private PlayerHealth _health;

    private void Awake()
    {
        // 컴포넌트 할당
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _health = GetComponent<PlayerHealth>();
        _eye = GetComponentInChildren<Camera>();
    }
    private void OnEnable()
    {
        // 초기화
        _applySpeed = _walkSpeed;
        _stamina = _initstamina;
        StaminaSlider.maxValue = _initstamina;
        StaminaSlider.value = _stamina;

    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        Move();
        RestoreStamina();
        CameraRotation();
        CharacterRotation();
    }

    // 지면 체크
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    // 점프 시도
    private void TryJump()
    {
        if (_input.Jump && isGround)
        {
            Jump();
        }
    }

    // 점프
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    // 달리기 시도
    private void TryRun()
    {
        if (_stamina > 0f)
        {
            if (_input.Run)
            {
                Running();
            }
            else
            {
                RunningCancel();
            }
        }
        else
            RunningCancel();
    }

    // 달리기
    private void Running()
    {
        isRun = true;
        _applySpeed = _runSpeed;
        _stamina -= Time.deltaTime;
        StaminaSlider.value = _stamina;
    }

    // 달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        _applySpeed = _walkSpeed;
    }
    private void Move()
    {
        float _moveDirX = _input.X;
        float _moveDirZ = _input.Y;

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * _applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = _input.MouseY;
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        _eye.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = _input.MouseX;
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    private void RestoreStamina()
    {
        if (_input.X==0&&_input.Y==0)
        {
            if (_stamina < _initstamina)
            {
                _stamina += Time.deltaTime * 0.5f;
                StaminaSlider.value = _stamina;
            }
        }
    }
}