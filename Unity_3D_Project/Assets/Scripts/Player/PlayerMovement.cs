using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 10f;

    [SerializeField]
    private float _runSpeed = 15f;

    [SerializeField]
    private float _applySpeed;

    // 스테미나
    [SerializeField]
    private float _stamina;
    private readonly float _initstamina = 10f;
    public Slider StaminaSlider;

    private bool isMove = false;

    private int _layerMask;
    // 민감도
    [SerializeField]
    private float lookSensitivity;

    // 카메라 한계
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    // 필요한 컴포넌트
    private Camera _eye;
    private Rigidbody _rigidbody;
    private PlayerInput _input;

    private void Awake()
    {
        _layerMask = 1 << (LayerMask.NameToLayer("Wall"));
        // 컴포넌트 할당
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
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
        CheckMove();
        TryRun();
        Move();
        RestoreStamina();
        CameraRotation();
        CharacterRotation();
    }


    private void CheckMove()
    {
        if (_input.X == 0 && _input.Y == 0)
        {
            isMove = false;
            _rigidbody.velocity = Vector3.zero;
        }
        else
            isMove = true;
    }

    // 달리기 시도
    private void TryRun()
    {
        if (_stamina > 0f && isMove)
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
        _applySpeed = _runSpeed;
        _stamina -= Time.deltaTime;
        StaminaSlider.value = _stamina;
    }

    // 달리기 취소
    private void RunningCancel()
    {
        _applySpeed = _walkSpeed;
    }
    private void Move()
    {
        float _moveDirX = _input.X;
        float _moveDirZ = _input.Y;

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 dir = (_moveHorizontal + _moveVertical).normalized;

        //Debug.DrawRay(transform.position,dir, Color.yellow,1f);
        if (Physics.Raycast(transform.position, dir, 1f, _layerMask))
        {
            return;
        }
        Vector3 _velocity = dir * _applySpeed;

        _rigidbody.MovePosition(transform.position + _velocity * Time.deltaTime);
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
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void RestoreStamina()
    {
        if (!isMove)
        {
            if (_stamina < _initstamina)
            {
                _stamina += Time.deltaTime * 2f;
                StaminaSlider.value = _stamina;
            }
        }
    }

    public void UseSnack()
    {
        StartCoroutine(InfinityStamina());
    }

    public IEnumerator InfinityStamina()
    {
        _stamina = float.MaxValue;
        StaminaSlider.value = _stamina;
        yield return new WaitForSeconds(5f);
        _stamina = _initstamina;
    }
}