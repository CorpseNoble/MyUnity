using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    public class AnimInputController : MonoBehaviour
    {

        [Header("Ranged fields")]
        [Range(0, 10)] public float jumpImpuls = 5f;
        [Range(0, 2)] public float dashTime = 1f;
        [Range(0, 10)] public float dashMulti = 5f;
        [Range(0, 10)] public float endComboDelay = 5f;
        [Range(0f, 1f)] public float currentFPRecoverDelay = 0.5f;
        [Range(0, 5)] public int currentFPRecoverAmount = 1;

        [Header("FP Cost")]
        [Range(0, 10)] public int attack_FPCost = 5;
        [Range(0, 10)] public int dash_FPCost = 10;
        [Range(0f, 1f)] public float run_FPCost = 0.1f;

        [Header("Component field")]
        [SerializeField] private PlayerInventoryData _playerInventory;
        [SerializeField] private DamageZone _getDamage;
        [SerializeField, HideInInspector] private PlayerInput _playerInput;
        [SerializeField, HideInInspector] private Rigidbody _rigidbody;
        [SerializeField, HideInInspector] private Animator _animator;


        [Header("Audio field")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _attackAudioClip;

        [Header("Anim variable name")]
        [SerializeField] private string _attackText = "Attack";
        [SerializeField] private string _vertText = "Forw";
        [SerializeField] private string _horText = "Right";
        [SerializeField] private string _shiftText = "Sprint";
        [SerializeField] private string _onOffText = "OnOff";
        [SerializeField] private string _onText = "On";
        [SerializeField] private string _speedMultiText = "SpeedMulti";

        [Header("Private Serialize Property field")]
        [SerializeField] private float _currentFP;
        [SerializeField] private float _horiz;
        [SerializeField] private float _vertical;
        [SerializeField] private bool _inputShift;
        [SerializeField] private bool _inputSpace;
        [SerializeField] private bool _onOff = true;
        [SerializeField] private bool _attack;
        [SerializeField] private int _comboCount;
        [SerializeField] private float _speedMultiplicator = 1;
        [SerializeField] private float _damageMultiplicator = 1;


        [Header("Unity Events")]
        public UnityEvent<BarStatType, int> FPChanged;

        [Header("Private delay")]
        [SerializeField] private float _waitEndCombo;
        [SerializeField] private float _waitEndFPRecovery;


        [HideInInspector] public Transform lookPoint;

        private float _rotationX = 0;
        private float sensivityMultiplicator;

        [SerializeField, Tooltip("Объек с камерой")] private Transform viewObject;
        [SerializeField, Tooltip("Какие слои не считать за землю")] private LayerMask ignoreMask;
        [SerializeField, Range(1, 20)] private float sensitivityHor = 9.0f;
        [SerializeField, Range(1, 20)] private float sensitivityVert = 9.0f;
        [SerializeField, Tooltip("Ограничение угла камеры снизу"), Range(-89, 0)] private float minimumVert = -45.0f;
        [SerializeField, Tooltip("Ограничение угла камеры сверху"), Range(0, 89)] private float maximumVert = 45.0f;

        #region Property

        public int ComboCount
        {
            get => _comboCount;
            set
            {
                if (_comboCount == value)
                    return;

                _comboCount = value;

                SpeedMultiplicator = 1 + (float)Math.Log10(1 + _comboCount);
                DamageMultiplicator = 1 + (float)Math.Sqrt(_comboCount);

                if (_comboCount > 0)
                    _waitEndCombo = endComboDelay;
            }
        }
        public float Horizontal
        {
            get => _horiz;
            set
            {
                if (_horiz == value)
                    return;

                _animator.SetFloat(_horText, value);
                _horiz = value;
            }
        }
        public float Vertical
        {
            get => _vertical;
            set
            {
                if (_vertical == value)
                    return;

                _animator.SetFloat(_vertText, value);
                _vertical = value;
            }
        }
        public bool InputShift
        {
            get => _inputShift;
            set
            {
                if (value)
                {
                    if (CurrentFP <= 0)
                    {
                        _animator.SetFloat(_shiftText, 1);
                        return;
                    }

                    CurrentFP -= run_FPCost;
                }

                if (_inputShift == value)
                    return;

                _inputShift = value;

                _animator.SetFloat(_shiftText, 1 + (_inputShift && CurrentFP > 0 ? 1 : 0));
            }
        }
        public bool InputSpace
        {
            get => _inputSpace;
            set
            {
                if (_inputSpace == value)
                    return;

                if (CurrentFP < dash_FPCost)
                    return;

                CurrentFP -= dash_FPCost;
                _inputSpace = value;
                _rigidbody.AddForce(Vector3.up * jumpImpuls * Physics.gravity.magnitude, ForceMode.Impulse);
            }
        }
        public bool OnOff
        {
            get => _onOff;
            set
            {
                if (value)
                {
                    _animator.SetBool(_onText, _onOff);
                    _animator.SetTrigger(_onOffText);
                    _onOff = !_onOff;
                }

            }
        }
        public bool Attack
        {
            get => _attack;
            set
            {
                if (OnOff)
                    return;

                //if (value)
                //{
                //    if (CurrentFP < attack_FPCost)
                //        return;
                //}

                //if (_attack == value)
                //    return;


                _attack = value && CurrentFP >= attack_FPCost;
                _animator.SetBool(_attackText, _attack);
            }
        }

        public float SpeedMultiplicator
        {
            get => _speedMultiplicator;
            set
            {
                if (_speedMultiplicator == value)
                    return;
                _speedMultiplicator = value;
                _animator.SetFloat(_speedMultiText, value);
            }
        }

        public float DamageMultiplicator
        {
            get => _damageMultiplicator;
            set
            {
                _damageMultiplicator = value;
            }
        }
        public bool InputDash
        {
            set
            {
                if (!value)
                    return;

                if (CurrentFP < dash_FPCost)
                    return;

                CurrentFP -= dash_FPCost;
                _animator.SetFloat(_shiftText, dashMulti);
                Invoke(nameof(DashEnd), dashTime);
            }
        }


        public float CurrentFP
        {
            get => _currentFP;
            set
            {
                if (value < -1)
                    return;

                if (_currentFP > value)
                    _waitEndFPRecovery = currentFPRecoverDelay;

                _currentFP = value;
                if (FPChanged.GetPersistentEventCount() > 0)
                    FPChanged.Invoke(BarStatType.FP, (int)value);
            }
        }
        public int MaxFP => _playerInventory.status[BarStatType.FP].Value;
        public bool InMenu { get => PlayerGamePrefs.InMenu; }
        public bool InDialogue { get => PlayerGamePrefs.InDialog; }



        #endregion

        void Start()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
                _rigidbody.freezeRotation = true;
            }
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
            }

            Cursor.lockState = CursorLockMode.Locked;

            if (lookPoint != null)
                lookPoint.parent = transform;

            sensivityMultiplicator = PlayerPrefs.GetFloat("Mouse", 0.5f);
            _getDamage.DamageValue = _playerInventory.status[SecStatType.PhAttack].Value;
            CurrentFP = MaxFP;
        }
        //void Update()
        //{
        //    if (!InMenu && !InDialogue)
        //    {
        //        InputKey();
        //    }
        //}

        public void OnCamera(InputValue value)
        {
            var v2 = value.Get<Vector2>();


            _rotationX -= v2.y * sensitivityVert * sensivityMultiplicator;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = v2.x * sensitivityHor * sensivityMultiplicator;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
            viewObject.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }

        public void OnMove(InputValue value)
        {
            var v2 = value.Get<Vector2>();

            Horizontal = v2.x;
            Vertical = v2.y;
        }

        public void OnSprint(InputValue value)
        {
            InputShift = value.Get<float>() > 0.5f;
        }

        public void OnJump(InputValue value)
        {
            InputSpace = value.Get<float>() > 0.5f;
        }

        public void OnUse(InputValue value)
        {
            InputDash = value.Get<float>() > 0.5f;
        }

        public void OnItem1(InputValue value)
        {
            Debug.Log("OnItem1: " + value.Get<float>());
        }
        public void OnItem2(InputValue value)
        {
            Debug.Log("OnItem2: " + value.Get<float>());
        }
        public void OnItem3(InputValue value)
        {
            Debug.Log("OnItem3: " + value.Get<float>());
        }
        public void OnItem4(InputValue value)
        {
            Debug.Log("OnItem4: " + value.Get<float>());
        }
        public void OnFirAttack(InputValue value)
        {
            Attack = value.Get<float>() > 0.5f;
        }
        public void OnSecAttack(InputValue value)
        {
            Debug.Log("OnSecAttack: " + value.Get<float>());
        }
        public void OnOnOff(InputValue value)
        {
            OnOff = value.Get<float>() > 0.5f;
        }

        private void FixedUpdate()
        {
            FPRecovery();
            ComboEnd();

        }
        private void FPRecovery()
        {
            if (CurrentFP >= MaxFP)
                return;

            if (_waitEndFPRecovery > 0)
            {
                _waitEndFPRecovery -= Time.fixedDeltaTime;
            }
            else if (_waitEndFPRecovery <= 0)
            {
                CurrentFP += currentFPRecoverAmount;
            }

        }
        private void ComboEnd()
        {
            if (ComboCount <= 0)
                return;

            if (_waitEndCombo <= 0)
            {
                ComboCount = 0;
                return;
            }

            _waitEndCombo -= Time.fixedDeltaTime;
        }

        private void AttackStart(float multi = 1)
        {
            CurrentFP -= attack_FPCost;
            _getDamage.Multi = multi * DamageMultiplicator;
            _audioSource.PlayOneShot(_attackAudioClip);
        }

        private void AttackEnd()
        {
            _getDamage.Multi = 0;
            _getDamage.EndHit(out int countHit);
            ComboCount += countHit;
        }

        private void DashEnd()
        {
            _animator.SetFloat(_shiftText, 1);
        }
    }
}
