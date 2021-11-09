using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    [SerializeField, Tooltip("Объек с камерой")] private Transform viewObject;
    [SerializeField, Tooltip("Какие слои не считать за землю")] private LayerMask ignoreMask;
    [SerializeField, Range(1, 20)] private float sensitivityHor = 9.0f;
    [SerializeField, Range(1, 20)] private float sensitivityVert = 9.0f;
    [SerializeField, Tooltip("Ограничение угла камеры снизу"), Range(-89, 0)] private float minimumVert = -45.0f;
    [SerializeField, Tooltip("Ограничение угла камеры сверху"), Range(0, 89)] private float maximumVert = 45.0f;

    [SerializeField] private bool debug = false;

    [HideInInspector] public Transform lookPoint;

    private bool inMenu = false;
    private bool inDialogue = false;
    private float _rotationX = 0;
    private float sensivityMultiplicator;


    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) body.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        //if(debug)
        //{
        //    lookPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        //    Destroy(lookPoint.GetComponent<SphereCollider>());
        //}
        //else
        //{
        //    lookPoint = new GameObject().transform;
        //}
        if (lookPoint != null)
            lookPoint.parent = transform;
        sensivityMultiplicator = PlayerPrefs.GetFloat("Mouse", 0.5f);
    }
    void LateUpdate()
    {
        if (!inMenu && !inDialogue)
        {
            Rotate();
            //RaycastLook();
        }
    }

    public void SmoothLookToTarget(Transform target)
    {
        Quaternion targetRot = Quaternion.LookRotation(target.position - viewObject.position);
        Quaternion currentRot = viewObject.rotation;

        StartCoroutine(ChangeView(currentRot, targetRot));
    }

    public void ReturnView()
    {
        viewObject.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }

    private void Rotate()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert * sensivityMultiplicator;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor * sensivityMultiplicator;
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        viewObject.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
    private void RaycastLook()
    {
        if (Physics.Raycast(viewObject.position, viewObject.forward, out RaycastHit hit, 500, ~ignoreMask))
        {
            lookPoint.position = hit.point;
        }
        else
        {
            lookPoint.position = viewObject.position + viewObject.forward * 100;
        }
    }

    public void SetDialogueState(bool inDialogueState)
    {
        inDialogue = inDialogueState;
    }
    private void OnChangeMouse(float value)
    {
        sensivityMultiplicator = value;
        PlayerPrefs.SetFloat("Mouse", sensivityMultiplicator);
    }

    private IEnumerator ChangeView(Quaternion currentRot, Quaternion targetRot)
    {
        float t = 0;

        while (t < 1)
        {
            viewObject.rotation = Quaternion.Lerp(currentRot, targetRot, t);
            t += Time.deltaTime;
            yield return null;
        }

        viewObject.rotation = targetRot;
    }
}
