using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour {
    [Header("InputSignals")]
    public float LSUp;
    public float LSRight;
    public float RSUp;
    public float RSRight;
    //public bool rt;
    //public bool rb;
    //public bool lt;
    //public bool lb;
    [Header("Signals")]
    public float Dmag;
    public Vector3 Dvec;
    public bool Jump;
    public bool run;
    public bool holdFire;
    public bool fire;
    public bool reload;
    public bool scope;
    public bool roll;
    public bool weaponOn = false;
    public float VerticalView;
    [Header("Other")]
    public bool inputEnabled = true;
    public float moveRate = 0.5f;

    protected float targetLSup;
    protected float targetLSright;
    protected float DrightSpeed;
    protected float DupSpeed;

    //protected bool OnRT;
    //protected bool OnLT;

    protected Vector2 SquareToCircle(Vector2 _input)
    {
        Vector2 result = Vector2.zero;

        result.x = _input.x * Mathf.Sqrt(1 - (_input.y * _input.y) / 2);
        result.y = _input.y * Mathf.Sqrt(1 - (_input.x * _input.x) / 2);

        return result;
    }

    //protected void LTorRT(float _getAxis)
    //{
    //    OnRT = false;
    //    OnLT = false;
    //    if (_getAxis > 0.19)
    //    {
    //        OnLT = true;
    //    }
    //    else if (_getAxis < -0.19)
    //    {
    //        OnRT = true;
    //    }
    //}

    protected void UpdateDmagDvec(float _Leftright, float _Leftup)
    {
        Vector2 temp = SquareToCircle(new Vector2(_Leftright, _Leftup));
        Dmag = Mathf.Sqrt((temp.y * temp.y) + (temp.x * temp.x));
        Dvec = temp.x * transform.right + temp.y * transform.forward;
    }

    //bug!!!!正负判断有问题!!
    //public void UpdateVerticalView(float _RightUp)
    //{
    //    float tempRotateX = VerticalViewRotateX;
    //    tempRotateX -= _RightUp;
    //    VerticalViewRotateX = Mathf.Clamp(tempRotateX, -90f, 90f);
    //}

}
