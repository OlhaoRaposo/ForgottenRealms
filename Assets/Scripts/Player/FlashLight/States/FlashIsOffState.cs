using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIsOffState : StateFSM
{
    private FlashLightFSM flashLight;
    public FlashIsOffState(
        FlashLightFSM flashLight){
        this.flashLight = flashLight;
    }
    private Light[] FlashLightLights;
    public void Enter()
    {
        FlashLightLights = flashLight.GetComponent<FlashLightFSM>().Lights;
        foreach (Light light in FlashLightLights)
        {
            light.enabled = false;
        }
    }
    public void Update()
    {
        
    }
    public void Exit()
    {
    }
}
