using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name = "Glock";
    //伤害
    public int damage = 5;
    //有效攻击距离
    public float range = 100f;
    //射速
    public float fireRate = 1f;
    //后坐力
    public float offSet = 5f;

    public GameObject graphics;
    private Animator WeaponAnim;

    public void SetAnim(Animator _anim)
    {
        WeaponAnim = _anim;
    }

    public Animator GetAnim()
    {
        if(WeaponAnim != null)
        {
            return WeaponAnim;
        }
        else
        {
            Debug.LogError("Can't Found Weapon Animator");
            return null;
        }
    }

    public void SetAnimatorTrigger(string _triggerName)
    {
        if (WeaponAnim != null)
        {
            WeaponAnim.SetTrigger(_triggerName);
        }
        else
        {
            Debug.LogError("No Weapon Animator");
        }
    }

    public void SetAnimatorBoolean(string _booleanName, bool _value)
    {
        if (WeaponAnim != null)
        {
            WeaponAnim.SetBool(_booleanName, _value);
        }
        else
        {
            Debug.LogError("No Weapon Animator");
        }
    }

    public bool GetAnimatorBoolean(string _booleanName)
    {
        if (WeaponAnim != null)
        {
            return WeaponAnim.GetBool(_booleanName);
        }
        else
        {
            Debug.LogError("No Weapon Animator");
            return false;
        }
    }
}
