using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstPersonControllerScript : MonoBehaviour
{
    public Camera Camera;
    [Range(0.01f, 2f)]
    public float Koef = 1f;
    [Range(0.1f, 2f)]
    public float sensitivity = 1f;

    [Range(1f, 20f)]
    public float speedNormal = 10f;
    [Range(1f, 20f)]
    public float speedRun = 20f;
    [Range(1f, 20f)]
    public float speedWalk = 5f;

    public KeyCode forwGo = KeyCode.W;
    public KeyCode backGo = KeyCode.S;
    public KeyCode rightGo = KeyCode.D;
    public KeyCode leftGo = KeyCode.A;
    public KeyCode SpeedUp = KeyCode.LeftShift;
    public KeyCode SpeedDown = KeyCode.LeftControl;

    [SerializeField]
    private Vector3 LookVector = Vector3.forward;
    [SerializeField]
    private Vector3 RightVector = Vector3.right;
    [SerializeField]
    private Vector3 CurrentVector = Vector3.zero;
    [SerializeField]
    private float CurrentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputUpdate();
    }
    public void InputUpdate()
    {
        if (Camera != null)
        {
            LookVector = Camera.transform.forward;
            LookVector.y = 0;
            LookVector.Normalize();

            RightVector = Camera.transform.right;
        }
        bool fg = Input.GetKey(forwGo);
        bool bg = Input.GetKey(backGo);
        bool rg = Input.GetKey(rightGo);
        bool lg = Input.GetKey(leftGo);
        bool su = Input.GetKey(SpeedUp);
        bool sd = Input.GetKey(SpeedDown);
        bool go = fg || bg || rg || lg;

        if (go)
        {
            Vector3 fv = fg ? LookVector : Vector3.zero;
            Vector3 bv = bg ? LookVector * -1 : Vector3.zero;
            Vector3 rv = rg ? RightVector : Vector3.zero;
            Vector3 lv = lg ? RightVector * -1 : Vector3.zero;

            CurrentVector = (fv + bv + rv + lv).normalized * CurrentSpeed * Koef;
        }
        else
        {
            CurrentVector = Vector3.zero;
        }

        if (su && !sd)
        {
            CurrentSpeed = speedRun;
        }
        else if (sd && !su)
        {
            CurrentSpeed = speedWalk;
        }
        else
        {
            CurrentSpeed = speedNormal;
        }

        transform.position += CurrentVector;


        var mh = Input.GetAxis("Horizontal"); // mouse Horizontal
        var mv = Input.GetAxis("Vertical"); // mouse Vertical

        //Camera.transform.Rotate(-Camera.transform.up * mh * sensitivity);
        //Camera.transform.Rotate(Camera.transform.right * mv * sensitivity);
    }
}
