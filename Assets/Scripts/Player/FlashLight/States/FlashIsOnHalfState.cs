using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIsOnHalfState : StateFSM
{
    private FlashLightFSM flashLight;
    public FlashIsOnHalfState(
        FlashLightFSM flashLight){
        this.flashLight = flashLight;

    }
    float flashTick = 0;
    float flashFailTime = 0;
    private bool tick = false;
    private Light[] FlashLightLights;
    public void Enter()
    {
        Debug.Log("Half");
        FlashLightLights = flashLight.GetComponent<FlashLightFSM>().Lights;
        foreach (Light light in FlashLightLights)
        {
            light.enabled = true;
        }
    }
    public void Update()
    {
        flashFailTime += Time.fixedDeltaTime;
        flashLight.flashLightBattery  -=Time.fixedDeltaTime;
        if (flashFailTime < 4) {
            return;
        }else if (flashFailTime >5 && flashFailTime < 7) {
            tick = true;
        }else if(flashFailTime > 8) {
            flashFailTime = 0;
            tick = false;
            foreach (Light light in FlashLightLights)
            {
                light.enabled = true;
            }
        }

        if (flashTick < .2f) {
            flashTick += Time.fixedDeltaTime;
        }else {
            if (tick) {
                foreach (Light light in FlashLightLights)
                {
                    light.enabled = !light.enabled;
                }
            }
            flashTick = 0;
        }
        
        if (flashLight.flashLightBattery <= 0) {
            flashLight.SetState(new FlashIsOffState(flashLight));
        }
    }
    public void Exit()
    {
    }
}
