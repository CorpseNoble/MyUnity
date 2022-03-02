using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimController : MonoBehaviour
    {
        public KeyCode sprint = KeyCode.LeftShift;
        public KeyCode onOff = KeyCode.Q;
        public KeyCode jump = KeyCode.Space;


        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _attackAudioClip;
        [SerializeField] private DamageZone _getDamage;
        [SerializeField] private Animator _animator;


        [SerializeField] private string _attackText = "Attack";
        [SerializeField] private string _vertText = "Forw";
        [SerializeField] private string _horText = "Right";
        [SerializeField] private string _shiftText = "Sprint";
        [SerializeField] private string _onOffText = "OnOff";
        [SerializeField] private string _onText = "On";
        [SerializeField] private string _speedMultiText = "SpeedMulti";

        [SerializeField] private float _horiz;
        [SerializeField] private float _vertical;
        [SerializeField] private bool _inputShift;
        [SerializeField] private bool _inputSpace;
        [SerializeField] private bool _onOff;
        [SerializeField] private bool _attack;
        [SerializeField] private int _comboCount;
        [SerializeField] private float _speedMultiplicator;
        [SerializeField] private float _damageMultiplicator;
        [SerializeField] private float _waitEndCombo;
        [SerializeField] private float _endComboDelay = 5f;

        public int ComboCount
        {
            get => _comboCount;
            set
            {
                _comboCount = value;
                SpeedMultiplicator = 1 + (float)Math.Log10(1 + value);
                DamageMultiplicator = 1 + (float)Math.Sqrt(value);

                if (value > 0)
                    _waitEndCombo = _endComboDelay;
            }
        }
        public float Horizontal
        {
            get => _horiz;
            set
            {
                _animator.SetFloat(_horText, value);


                _horiz = value;
            }
        }
        public float Vertical
        {
            get => _vertical;
            set
            {
                _animator.SetFloat(_vertText, value);

                _vertical = value;
            }
        }
        public bool InputShift
        {
            get => _inputShift;
            set
            {
                _animator.SetFloat(_shiftText, 1 + (value ? 1 : 0));
                _inputShift = value;
            }
        }
        public bool InputSpace
        {
            get => _inputSpace;
            set
            {
                _inputSpace = value;
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
                _attack = value;
                _animator.SetBool(_attackText, value);
            }
        }

        public float SpeedMultiplicator
        {
            get => _speedMultiplicator;
            set
            {
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

        void Start()
        {
        }

        void Update()
        {
            InputKey();
            ComboEnd();
        }

        private void InputKey()
        {
            Attack = Input.GetMouseButton(0);
            OnOff = Input.GetKeyDown(onOff);
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            InputShift = Input.GetKey(sprint);
            InputSpace = Input.GetKeyDown(jump);
        }

        private void AttackStart(float multi = 1)
        {
            _getDamage.Multi = multi * DamageMultiplicator;
            _audioSource.PlayOneShot(_attackAudioClip);
        }

        private void AttackEnd()
        {
            _getDamage.Multi = 0;
            _getDamage.EndHit(out int countHit);
            ComboCount += countHit;
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

            _waitEndCombo -= Time.deltaTime;
        }
    }
    [Serializable]
    public class AmimInput
    {
        public Animator animator;

        public string variableName;
        public KeyCode key;
        public Type variableType;
        public bool downOn;
        public bool setTrigger;
        public string triggerName;
        [SerializeField] private bool _valBool;

        public AmimInput(Animator animator)
        {
            this.animator = animator;
        }
        public bool Value
        {
            set
            {
                switch (variableType)
                {
                    case Type.Bool:
                        ValBool = value;
                        break;
                    case Type.Float:
                        ValFloat = value;
                        break;
                    case Type.Int:
                        ValInt = value;
                        break;
                    case Type.Trigger:
                        ValTrigger = value;
                        break;
                }
                if (value)
                    _valBool = !_valBool;
            }
        }
        public bool ValBool
        {
            set
            {
                if (!setTrigger)
                    animator.SetBool(variableName, value);
                if (setTrigger && value)
                {
                    animator.SetBool(variableName, _valBool);
                    animator.SetTrigger(triggerName);
                }
            }
        }
        public bool ValInt
        {
            set
            {
                animator.SetInteger(variableName, (value ? 1 : 0));
                if (setTrigger && value)
                {
                    animator.SetTrigger(triggerName);
                }
            }
        }
        public bool ValFloat
        {
            set
            {
                animator.SetFloat(variableName, (value ? 1 : 0));
                if (setTrigger && value)
                {
                    animator.SetTrigger(triggerName);
                }
            }
        }
        public bool ValTrigger
        {
            set
            {
                if (value)
                {
                    animator.SetTrigger(variableName);
                }
            }
        }

        public void Check()
        {
            if (downOn)
                Value = Input.GetKeyDown(key);
            else
                Value = Input.GetKey(key);
        }
        public enum Type
        {
            Int,
            Float,
            Bool,
            Trigger,
        }
    }
}
