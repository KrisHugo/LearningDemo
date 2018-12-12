using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public PlayerInfo pi;

    public WeaponController wc;

    private Rigidbody rb;
    private CapsuleCollider col;
    [SerializeField]
    private bool isGround = false;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 13000f;
    
    private KeyBoardInput userInput;
    private PlayerMotor motor;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInfo>();
        userInput = GetComponent<KeyBoardInput>();
        wc = GetComponent<WeaponController>();
        motor = GetComponent<PlayerMotor>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        pi.OnDeath += OnPlayerDead;
        pi.OnRespawn += OnPlayerRespawn;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Input&Controller

        motor.Move(userInput.Dvec * speed);
        //Apply rotation
        motor.Rotate(new Vector3(0f, userInput.RSRight, 0f) * lookSensitivity);
        motor.RotateCamera(userInput.RSUp * lookSensitivity);
        
        Vector3 _thrusterForce = Vector3.zero;
        if (isGround)
        {
            if (userInput.Jump)
            {
                _thrusterForce = Vector3.up * thrusterForce;
            }
        }

        //Apply the thrust force
        motor.ApplyThruster(_thrusterForce);
        //attack
        if (userInput.fire)
        {
            wc.Shoot(out float offset);
            motor.AddCameraOffset(offset);    
        }
        else
        {
            motor.ResetCameraOffset();
        }

        //scope
        if (userInput.scope)
        {
            wc.ScopeOnOff();
        }

        
        #endregion
    }

    void OnPlayerRespawn()
    {
        rb.isKinematic = false;
        col.enabled = true;
        PhotonView pv = GetComponent<PhotonView>();
        if (pv.isMine)
        {
            enabled = true;
        }
        gameObject.transform.position = new Vector3(0f, 5f, 0f);
        gameObject.transform.rotation = Quaternion.identity;
    }

    void OnPlayerDead()
    {
        rb.isKinematic = true;
        col.enabled = false;
        PhotonView pv = GetComponent<PhotonView>();
        if (pv.isMine)
        {
            enabled = false;
        }
    }

}
