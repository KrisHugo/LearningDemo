using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyTimer
{
    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED,
    }

    public float duration = 1.0f;

    public float elapsedTime = 0;
    public STATE state = STATE.IDLE;
    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= duration)
                {
                    state = STATE.FINISHED;
                }
                break;
            case STATE.FINISHED:
                break;
            default:
                Debug.LogErrorFormat("MyTimerError");
                break;
        }

    }

    public void Go()
    {
        elapsedTime = 0;
        state = STATE.RUN;
    }

}
