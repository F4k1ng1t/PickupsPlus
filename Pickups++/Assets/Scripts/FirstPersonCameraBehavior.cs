using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class FirstPersonCameraBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;


    private float vInput;
    private float hInput;

    private Rigidbody _rb;


    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    public Camera Main_Camera;


    public bool lockCursor = true;
    

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    public bool playerCanMove = true;
   

    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {

        vInput = UnityEngine.Input.GetAxis("Vertical") * moveSpeed;
        hInput = UnityEngine.Input.GetAxis("Horizontal") * moveSpeed;

        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * UnityEngine.Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            Main_Camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }
    
    void FixedUpdate()
    {

        _rb = GetComponent<Rigidbody>();

        //Vector3 rotation = Vector3.up * hInput;

        //Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(transform.position + (transform.forward * vInput + this.transform.right * hInput) * Time.fixedDeltaTime);

        
        
    }
}
