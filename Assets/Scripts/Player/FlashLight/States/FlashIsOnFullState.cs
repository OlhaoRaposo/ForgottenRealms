using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIsOnFullState : StateFSM
{
    private FlashLightFSM flashLight;
    public FlashIsOnFullState(
        FlashLightFSM flashLight){
        this.flashLight = flashLight;
    }
    private Light[] FlashLightLights;
    public void Enter()
    {
        FlashLightLights = flashLight.GetComponent<FlashLightFSM>().Lights;
        foreach (Light light in FlashLightLights)
        {
            light.enabled = true;
            light.intensity = 9f;
        }
    }
    public void Update()
    {
        if (flashLight.bState == FlashLightFSM.FlashLightBateryStates.isFull) {
            flashLight.flashLightBattery  -=Time.fixedDeltaTime;
        }else if(flashLight.bState == FlashLightFSM.FlashLightBateryStates.isHalf) {
            flashLight.SetState(new FlashIsOnHalfState(flashLight));
        }else if (flashLight.bState == FlashLightFSM.FlashLightBateryStates.isEnd) {
            flashLight.SetState(new FlashIsOffState(flashLight));
        }else if (flashLight.bState == FlashLightFSM.FlashLightBateryStates.isLow) {
            flashLight.SetState(new FlashLightIsOnLowState(flashLight));
        }

    }
    public void Exit()
    {
    }
}


