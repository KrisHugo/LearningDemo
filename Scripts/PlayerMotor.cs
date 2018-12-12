using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private CameraController camCtrl;
    //[SerializeField]
    //private Camera cam;
    [SerializeField]
    private float maxEularX = -90f;
    [SerializeField]
    private float minEularX = 85f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    private Rigidbody rb;
    [Header("CameraOffsetSetting")]
    private float cameraOffsetX = 0f;
    private float currentCameraOffsetX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetCameraController(CameraController _camCon)
    {
        camCtrl = _camCon;
    }


    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        FireOffset();
    }

    //Gets a Movement vector;
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void AddCameraOffset(float _cameraOffsetX)
    {
        cameraOffsetX += _cameraOffsetX;
    }

    public void ResetCameraOffset()
    {
        cameraOffsetX = 0;
    }

    //Gets a force vector based on velocity variable
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }
    

    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void FireOffset()
    {
        currentCameraOffsetX = Mathf.Lerp(currentCameraOffsetX, cameraOffsetX, Time.deltaTime * 10f);
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(camCtrl != null)
        {
            //后坐力系统还有很多问题，偶尔这个系统会突然失效一两次。可能是update的问题。而且没有复位系统
            currentCameraRotationX -= cameraRotationX + currentCameraOffsetX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, maxEularX, minEularX);
            camCtrl.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

}
