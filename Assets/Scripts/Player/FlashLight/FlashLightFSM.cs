using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightFSM : MonoBehaviour
{
    StateFSM state;
    [Header("FlashLightState")]
    public FlashLightBateryStates bState;
    public enum  FlashLightBateryStates {
        isFull,isHalf,isLow,isEnd
    }

    public Light[] Lights;
    [SerializeField] public float flashLightBattery = 100f;
    [SerializeField] private float batery;

    void Start()
    {
        SetState(new FlashIsOffState(this));
        batery = flashLightBattery;
    }
    void Update()
    {
        CalculateBattery();
        TurnFlashLightOn();
        
        state?.Update();
    }

    private void TurnFlashLightOn()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (bState == FlashLightBateryStates.isHalf)
            {
                SetState(new FlashIsOnHalfState(this));
            }
            else if (bState == FlashLightBateryStates.isLow)
            {
                SetState(new FlashLightIsOnLowState(this));
            }
            else if (bState == FlashLightBateryStates.isFull)
            {
                SetState(new FlashIsOnFullState(this));
            }
            else if (bState == FlashLightBateryStates.isEnd)
            {
                SetState(new FlashIsOffState(this));
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            SetState(new FlashIsOffState(this));
        }
    }

    private void CalculateBattery()
    {
        if (flashLightBattery > batery / 2) {
            //FullBatery
            bState = FlashLightBateryStates.isFull;
        }else if (flashLightBattery < batery / 2 && flashLightBattery > batery * .2f)
        {
            //HalfBatery
            bState = FlashLightBateryStates.isHalf;
        }else if (flashLightBattery < batery * .2f && flashLightBattery > .1f)
        {
            //LowBatery
            bState = FlashLightBateryStates.isLow;
        }else if(flashLightBattery <= 0){
            //BateryIsOver
            bState = FlashLightBateryStates.isEnd;
        }
    }
    public void SetState(StateFSM state) {
        state?.Exit();
        this.state = state;
        state?.Enter();
    }
}
