using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : IUserInput
{
    [Header("Key settings")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyRight = "d";
    public string KeyLeft = "a";

    public string buttonA;
    public string buttonB;
    public string buttonC;
    public string buttonD;
    public string buttonE;
    public string buttonF;
    public string buttonG;
    public string buttonH;
    public string buttonI;

    public string KeyJup;
    public string KeyJdowm;
    public string KeyJright;
    public string KeyJleft;

    private MyButton btnA = new MyButton();
    private MyButton btnB = new MyButton();
    private MyButton btnC = new MyButton();
    private MyButton btnD = new MyButton();
    private MyButton btnE = new MyButton();
    private MyButton btnF = new MyButton();
    private MyButton btnG = new MyButton();
    private MyButton btnH = new MyButton();
    private MyButton btnI = new MyButton();

    [Header("Mouse settings")]
    public bool mouseEnabled = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;


    // Update is called once per frame
    void Update()
    {
        btnA.Tick(Input.GetKey(buttonA));
        btnB.Tick(Input.GetKey(buttonB));
        //btnC.Tick(Input.GetKey(buttonC));
        //btnD.Tick(Input.GetKey(buttonD));
        //btnE.Tick(Input.GetKey(buttonE));
        //btnF.Tick(Input.GetKey(buttonF));
        //btnG.Tick(Input.GetKey(buttonG));
        //btnH.Tick(Input.GetKey(buttonH));
        //btnI.Tick(Input.GetKey(buttonI));

        if (mouseEnabled)
        {
            RSUp = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            RSRight = Input.GetAxis("Mouse X") * mouseSensitivityX;
        }
        else
        {
            RSUp = (Input.GetKey(KeyJup) ? 1.0f : 0) - (Input.GetKey(KeyJdowm) ? 1.0f : 0);
            RSRight = (Input.GetKey(KeyJright) ? 1.0f : 0) - (Input.GetKey(KeyJleft) ? 1.0f : 0);
        }
        
        targetLSup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        targetLSright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        if (!inputEnabled)
        {
            targetLSup = 0;
            targetLSright = 0;
        }

        LSUp = Mathf.SmoothDamp(LSUp, targetLSup, ref DrightSpeed, moveRate);
        LSRight = Mathf.SmoothDamp(LSRight, targetLSright, ref DupSpeed, moveRate);

        UpdateDmagDvec(LSRight,LSUp);

        //if (inputEnabled)
        //{
        //    if (Input.GetKeyDown(buttonB))
        //    {
        //        weaponOn = !weaponOn;
        //    }
        //}

        Jump = btnA.OnPressed;
        //holdFire = btnB.IsPressing;
        fire = Input.GetKeyDown(buttonB);
        scope = Input.GetKeyDown(buttonC);
        
        //roll = (btnA.isExtending && btnA.IsPressing);
        //run = ((btnA.IsPressing && !btnA.isDelaying) || btnA.isExtending);
        //attack = btnG.OnPressed;
        //run = btnA.IsPressing;
    }

}
