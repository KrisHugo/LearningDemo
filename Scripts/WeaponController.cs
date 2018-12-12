using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
public class WeaponController : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";

    [SerializeField]
    public WeaponData currentWeapon;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;
    private WeaponManager weaponManager;

    private float lastFireTime = 0f;
    public void Setup(Camera _weaponCamera)
    {
        cam = _weaponCamera;
        if (cam == null)
        {
            Debug.LogError("WeaponController: " + "No Camera Referenced");
            enabled = false;
        }
    }

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
    }


    public void Shoot(out float _cameraOffSet)
    {
        if (Time.time > lastFireTime)
        {
            lastFireTime = Time.time + (1f/currentWeapon.fireRate);
            PhotonView pv = GetComponent<PhotonView>();
            pv.RPC("RPC_Fire", PhotonTargets.All);
            _cameraOffSet = currentWeapon.offSet;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit _hit, currentWeapon.range, mask))
            {
                Debug.Log("hited! we hit " + _hit.collider.name);
                if (_hit.collider.tag == PLAYER_TAG)
                {
                    PhotonView getHit = _hit.collider.GetComponent<PhotonView>();
                    getHit.RPC("RPC_GetDamage", PhotonTargets.All, currentWeapon.damage, GetComponent<PhotonView>().viewID);
                }
                pv.RPC("RPC_PlayHitEffect", PhotonTargets.All, _hit.point, _hit.normal);
            }
        }
        else
        {
            _cameraOffSet = 0;
            Debug.Log("Doesn't cooldown the weapon!" + " Time = " + Time.time + ", fireRate = " + (1 / currentWeapon.fireRate));

        }
    }
    
    public void ScopeOnOff()
    {
        currentWeapon.SetAnimatorBoolean("Aim", !currentWeapon.GetAnimatorBoolean("Aim"));
    }
    

    [PunRPC]
    private void RPC_PlayHitEffect(Vector3 _position, Vector3 _normal)
    {
        GameObject _hitEffectIns = Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _position, Quaternion.LookRotation(_normal));
        Destroy(_hitEffectIns, 2f);
    }


    [PunRPC]
    private void RPC_Fire()
    {
        currentWeapon.SetAnimatorTrigger("Fire");
        //FireFX
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }

    

}
