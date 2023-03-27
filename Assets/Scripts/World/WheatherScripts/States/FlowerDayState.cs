using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDayState : StateFSM
{
    private WheatherManagerFSM wheatherManager; 
    public FlowerDayState(
        WheatherManagerFSM wheatherManager){
        this.wheatherManager = wheatherManager;
    }
    private GameObject particle;
    public void Enter()
    {
        Debug.Log("Enter FlowersDay");
        
        if (wheatherManager.atualEvent.dayParticles.gameObject != null && particle == null) {
            particle = wheatherManager.atualEvent.dayParticles.gameObject;
        }
        foreach (Events events in wheatherManager.atualWheather.events)
        {
            if (events.dayParticles != particle) {
                events.dayParticles.SetActive(false);
            }
        }
        if (particle.activeSelf == false) {
            particle.gameObject.SetActive(true);
        }
    }
    public void Update()
    {
    }
    public void Exit()
    {
       
    }
}
