using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts;
using Unity.VisualScripting;
using System;

public class Person : Character
{
    private static UpgradableParametr runSpeed;

    [SerializeField]
    private float walkSpeed = 1.5f;

    [SerializeField]
    private float movementTransitionSpeed = 8f;

    [SerializeField]
    private float _gravity = 11f;

    [SerializeField]
    private float smoothTime = 0.03f;

    private float _currentSpeed = 0.0f;


    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _currentVelocity;
    private bool _isSitting = false;
    private bool _isAttacking = false;
    private List<Weapon> _weaponList = new();
    private Weapon _currentWeapon;
    private MagicFire _magicFire;
    private MeeleSword _meeleSword;
    private MeeleAxe _meeleAxe;
    private MagicWind _magicWind;
    private int _currentWeaponIndex = 0;
    private List<GameObject> _weaponObjects = new();
    private AnimatorStateInfo _stateInfo;
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator = GetComponentInChildren<Animator>();
        _magicFire = GetComponent<MagicFire>();
        _meeleSword = GetComponent<MeeleSword>();
        _meeleAxe = GetComponent<MeeleAxe>();
        _magicWind = GetComponent<MagicWind>();
        _weaponList.Add(_meeleSword);
        _weaponList.Add(_meeleAxe);
        _weaponList.Add(_magicFire);
        _weaponList.Add(_magicWind);
        _currentWeapon = _weaponList[0];
        _weaponObjects.Add(GameObject.FindWithTag("Sword"));
        _weaponObjects.Add(GameObject.FindWithTag("Axe"));
        _weaponObjects.Add(GameObject.FindWithTag("FireStaff"));
        _weaponObjects.Add(GameObject.FindWithTag("WindStaff"));
        _weaponObjects[1].SetActive(false);
        _weaponObjects[2].SetActive(false);
        _weaponObjects[3].SetActive(false);
        _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        runSpeed = ResetUpgradbleParam("speed");
    }

    private UpgradableParametr ResetUpgradbleParam(string perString)
    {
        UpgradableParametr perParam = new();
        perParam._current = PersonUPConfig.personLevels[perString][0];
        perParam._currentLvl = 0;
        perParam._lvlsDictionary = PersonUPConfig.personLevels[perString];
        return perParam;
    }

    public static UpgradableParametr Upgrade(string param)
    {
        if (param == "speed")
        {
            return UpgradeByParam(runSpeed);
        }
        throw new NotImplementedException();
    }
    private static UpgradableParametr UpgradeByParam(UpgradableParametr perParam)
    {
        perParam._currentLvl += 1;
        perParam._current = perParam._lvlsDictionary[perParam._currentLvl];
        return perParam;
    }

    void Update()
    {
        InputDirection();
        InputChangeWeapon();
        InputAttack();
    }
    void IsAttacking()
    {
        if (_stateInfo.IsName("1H_Melee_Attack_Slice_Diagonal")
            || _stateInfo.IsName("Spellcast_Shoot")
            || _stateInfo.IsName("2H_Melee_Attack_Spin"))
        {
            _isAttacking = true;
            return;
        }
        _isAttacking = false;
    }
    void FixedUpdate()
    {
        Move();
    }
    void InputChangeWeapon()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta > 0)
        {
            _weaponObjects[_currentWeaponIndex].SetActive(false);
            _currentWeaponIndex += 1;
            _currentWeaponIndex = _currentWeaponIndex % 4;
            _currentWeapon = _weaponList[_currentWeaponIndex];
            _weaponObjects[_currentWeaponIndex].SetActive(true);
            //_animator.SetTrigger("ChangeWeapon");
        }
        else if (scrollDelta < 0)
        {
            _weaponObjects[_currentWeaponIndex].SetActive(false);
            _currentWeaponIndex -= 1;
            if (_currentWeaponIndex < 0) _currentWeaponIndex = 3;
            _currentWeapon = _weaponList[_currentWeaponIndex];
            _weaponObjects[_currentWeaponIndex].SetActive(true);
            //_animator.SetTrigger("ChangeWeapon");
        }
    }
    protected private void Move()
    {
        IsAttacking();
        if (!_isSitting && !_isAttacking)
        {
            Movement(_moveDirection);
            Rotation(_moveDirection);
        }
        if (_moveDirection == Vector3.zero)
            StartCoroutine(Sitting());
        GravityMovement(_characterController.isGrounded);
    }
    void InputDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _moveDirection = new Vector3(x, 0.0f, z);
    }
    void InputAttack()
    {
        _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetMouseButtonDown(0)) // && !_animator.GetBool(_currentWeapon._animationName)) { // :)
        {
            if (_weaponList[_currentWeaponIndex].GetIsReloading) return;
            _currentWeapon.Attack();
            _animator.SetTrigger(_currentWeapon._animationName);
        }
    }
    void Movement(Vector3 direction)
    {
        //_currentSpeed = Mathf.SmoothStep(_currentSpeed, walkSpeed, 2 * Time.deltaTime);
        if (direction != Vector3.zero && _animator.GetBool("IsWalking") && Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, runSpeed._current, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", true);
        }
        else if (direction != Vector3.zero)
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, walkSpeed, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, 0, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", false);
        }


        _characterController.Move(direction * _currentSpeed * Time.deltaTime);
    }

    IEnumerator Sitting()
    {
        bool isAnimSitting = _animator.GetBool("IsSitting");
        if (Input.GetKey(KeyCode.C) && !isAnimSitting)
        {
            _isSitting = true;
            _animator.SetBool("IsSitting", true);
        }
        else if (Input.anyKey && !Input.GetKey(KeyCode.C) && _isSitting && isAnimSitting)
        {
            _animator.SetBool("IsSitting", false);
            _currentSpeed = 0;
            yield return new WaitForSeconds(0.6f);
            _isSitting = false;
        }
    }

    void GravityMovement(bool isGrounded)
    {
        if (isGrounded && _velocity.y < 0.0f)
            _velocity.y = -1f;
        else
        {
            _velocity.y -= _gravity * Time.fixedDeltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
    }

    void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
