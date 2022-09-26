using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{

    [RequireComponent(typeof(CharacterController))]
    public class InputMove : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private float speed = 5f;
        [SerializeField, Range(1, 5), Tooltip("Ускорение при рывке")] private float sprintMultiplicator = 3;

        private CharacterController charController;
        private Vector3 moveVector;
        [SerializeField] private float sprintMultiplicatorBufer = 1;

        public bool InMenu { get => PlayerGamePrefs.InMenu; set => PlayerGamePrefs.InMenu = value; }
        public bool InDialogue { get => PlayerGamePrefs.InDialog; set => PlayerGamePrefs.InDialog = value; }

        private void Start()
        {
            charController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            if (!InMenu && !InDialogue)
            {
                PlayerMove();
            }
        }



        private void PlayerMove()
        {

            moveVector = new Vector3(Horizontal, 0, Vertical).normalized;
            moveVector = moveVector * speed * sprintMultiplicatorBufer * Time.deltaTime;

            moveVector = transform.TransformDirection(moveVector);

            charController.Move(moveVector);
        }


        [SerializeField] private bool _inputShift;

        public bool InputShift
        {
            get => _inputShift;
            set
            {
                _inputShift = value;

                sprintMultiplicatorBufer = value ? Mathf.Lerp(1, sprintMultiplicator, sprintMultiplicatorBufer) : 1;
            }
        }

        [SerializeField] private float _horiz;
        [SerializeField] private float _vertical;

        public float Horizontal
        {
            get => _horiz;
            set
            {
                if (_horiz == value)
                    return;

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

                _vertical = value;
            }
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

        private Vector2 v2;
        private float _rotationX = 0;
        private float sensivityMultiplicator = 0.5f;

        [SerializeField, Tooltip("Объек с камерой")] private Transform viewObject;
        [SerializeField, Tooltip("Какие слои не считать за землю")] private LayerMask ignoreMask;
        [SerializeField, Range(1, 20)] private float sensitivityHor = 9.0f;
        [SerializeField, Range(1, 20)] private float sensitivityVert = 9.0f;
        [SerializeField, Tooltip("Ограничение угла камеры снизу"), Range(-89, 0)] private float minimumVert = -45.0f;
        [SerializeField, Tooltip("Ограничение угла камеры сверху"), Range(0, 89)] private float maximumVert = 45.0f;


        public void OnCamera(InputValue value)
        {
            v2 = value.Get<Vector2>();
            if (!InMenu && !InDialogue)
            {
                CameraMove();
            }
        }

        private void CameraMove()
        {
            _rotationX -= v2.y * sensitivityVert * sensivityMultiplicator;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = v2.x * sensitivityHor * sensivityMultiplicator;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
            viewObject.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
    }
}
