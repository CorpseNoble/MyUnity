using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GenPr1
{

public class Char1ControllerScript : MonoBehaviour
{
    //CharacterController characterController;
    Animator animator;
    PlayerInputs playerInputs = new PlayerInputs();

    [Range(0f, 8f)]
    public float speed = 0.5f;

    public GameObject mainCamera;
    public float SpeedV = 2;
    public float SpeedH = 2;

    private float yaw = 0f;
    private float pitch = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        // characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CatchButton();
        //MouseCameraController();
    }
    void FixedUpdate()
    {

        if (Input.GetKey(playerInputs.GoUp.key))
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        if (Input.GetKey(playerInputs.GoDown.key))
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
    }

    private void MouseCameraController()
    {
        yaw += Input.GetAxis("Mouse X") * SpeedH;
        pitch -= Input.GetAxis("Mouse Y") * SpeedV;

        mainCamera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    public void CatchButton()
    {
        ControllerMethods.SetFloat(playerInputs.GoFront.key, playerInputs.GoBack.key, "Speed", animator);
        ControllerMethods.SetFloat(playerInputs.RotateRight.key, playerInputs.RotateLeft.key, "Rotate", animator);

        //SetFloat(playerInputs.GoUp.key, playerInputs.GoDown.key, "Falling");

        ControllerMethods.SetBool(playerInputs.Block.key, "Block", animator);

        ControllerMethods.SetTrigger(playerInputs.Attack.key, "Attacking", animator);

        ControllerMethods.SetTrigger(playerInputs.Action.key, "OpenChest", animator);
    }
}
}
