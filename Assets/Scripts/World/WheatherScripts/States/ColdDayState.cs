using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDayState : StateFSM
{
    private WheatherManagerFSM wheatherManager; 
    public ColdDayState(
        WheatherManagerFSM wheatherManager){
        this.wheatherManager = wheatherManager;
    }
    private GameObject particle;
    public void Enter()
    {
        Debug.Log("Enter ColdDay");
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
