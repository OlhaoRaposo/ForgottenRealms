using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightIsOnLowState : StateFSM
{
    private FlashLightFSM flashLight;
    public FlashLightIsOnLowState(
        FlashLightFSM flashLight){
        this.flashLight = flashLight;

    }
    
    private float flashTick = 0;
    private float flashFailTime = 0;
    private bool tick = false;
    private Light[] FlashLightLights;
    public void Enter()
    {
        Debug.Log("Low");
        FlashLightLights = flashLight.GetComponent<FlashLightFSM>().Lights;
        foreach (Light light in FlashLightLights)
        {
            light.enabled = true;
            light.intensity = 4f;
        }
    }
    public void Update()
    {
        flashFailTime += Time.fixedDeltaTime;
        if (flashFailTime < 4) {
            return;
        }else if (flashFailTime > 5 && flashFailTime < 7) {
            tick = true;
        }else if(flashFailTime > 12) {
            tick = false;
            //Make sure that light always ends on on Tick
            foreach (Light light in FlashLightLights)
            {
                light.enabled = true;
            }
            flashFailTime = 0;
        }

        if (flashTick < .2f) {
            flashTick += Time.fixedDeltaTime;
        }else {
            if (tick) {
                //Make the lights tick 
                foreach (Light light in FlashLightLights)
                {
                    light.enabled = !light.enabled;
                }
            }
            flashTick = 0;
        }
        
        if (flashLight.flashLightBattery > 0) {
            flashLight.flashLightBattery  -=Time.fixedDeltaTime;
        }else {
            flashLight.SetState(new FlashIsOffState(flashLight));
        }
    }
    public void Exit()
    {
    }
}