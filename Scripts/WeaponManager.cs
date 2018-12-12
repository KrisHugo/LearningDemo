using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    public string LocalWeapon_LayerName = "Weapon";
    [SerializeField]
    public string RemoteWeapon_LayerName = "RemotePlayer";

    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private WeaponData PrimaryWeapon;


    private WeaponData currentWeapon;
    private WeaponGraphics currentGraphics;

    void Awake()
    {
        EquipWeapon(PrimaryWeapon);
    }

    public WeaponData GetCurrentWeapon()
    {
        return currentWeapon;

    }
    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipWeapon(WeaponData _newWeapon)
    {
        currentWeapon = _newWeapon;

        GameObject _weaponIns = Instantiate(_newWeapon.graphics, weaponHolder);
        //_weaponIns.transform.localScale *= 1.5f;
        currentWeapon.SetAnim(_weaponIns.GetComponent<Animator>());
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();

        if(currentGraphics == null)
        {
            Debug.LogError("No Weapon Graphics On the component on the weapon object: " + _weaponIns.name);
        }
        
        if (GetComponent<PhotonView>().isMine)
        {
            Debug.Log("Setting Local Graphics");
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(LocalWeapon_LayerName));
        }
        else
        {
            Debug.Log("Setting Remote Graphics");
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(RemoteWeapon_LayerName));
        }
    }


}
