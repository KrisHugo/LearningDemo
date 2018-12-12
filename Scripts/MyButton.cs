using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton {
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnRelesed = false;
    public bool isExtending = false;
    public bool isDelaying = false;

    public float extendingDuration = 0.2f;
    public float delayTimerDuration = 0.5f;
    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool _input)
    {
        extTimer.Tick();
        delayTimer.Tick();
        curState = _input;

        IsPressing = curState;
        OnPressed = false;
        OnRelesed = false;
        isExtending = false;
        isDelaying = false;

        if(curState != lastState)
        {
            if(curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayTimerDuration);
            }
            else
            {
                OnRelesed = true;
                StartTimer(extTimer, extendingDuration);
            }
        }
        lastState = curState;

        if(extTimer.state == MyTimer.STATE.RUN)
        {
            isExtending = true;
        }
        if(delayTimer.state == MyTimer.STATE.RUN)
        {
            isDelaying = true;
        }
    }

    private void StartTimer(MyTimer _timer, float _duration)
    {
        _timer.duration = _duration;
        _timer.Go();
    }
}
