using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WheatherManagerFSM : MonoBehaviour
{
    StateFSM state;
    [Header("MonthSystem")]
    [SerializeField]
    public MonthStats atualWheather;

    [Header("DaypercentTrigger")] 
    [SerializeField]
    public Events atualEvent;
    public int[] dayTrigger;
    private void Start()
    {
        DayTriggerRnd();
    }

    void Update()
    {
        state?.Update();
    } 
    public void SetState(StateFSM state)
     {
         state?.Exit();
         this.state = state;
         state?.Enter();
     }
    //Escolhe a probabilidade do dia dependendo do mes
     private void SetWheather()
     {
         DayTriggerRnd();
         int eventPercent = 0;
         eventPercent = dayTrigger[0];
         //PrincipalEvent
         foreach (Events getEvent in atualWheather.events)
         {
             if (Enumerable.Range(getEvent.minTrigger, getEvent.maxTrigger).Contains(eventPercent))
             {
                     atualEvent = getEvent;
                     Debug.Log(atualEvent.EventName);
                     TurnOnEvent(getEvent.EventName);
             }
         }
     }
     private void DayTriggerRnd()
     {
         int newDaytrigger;
         for (int j = 0; j < dayTrigger.Length; j++)
         {
             if (dayTrigger[j] > 100)
             {
                 dayTrigger[j] = Random.Range(0, 101);
             }
         }
         newDaytrigger = Random.Range(0, 101);
         dayTrigger[0] = dayTrigger[1];
         dayTrigger[1] = newDaytrigger;
     }
     
     //Seta o stateMachine pro dia certo
     private void TurnOnEvent(string eventDay)
     {
         switch (eventDay)
         {
             case "RainEvent":
                 SetState(new RainDayState(this));
                 break;
             case "WindEvent":
                 SetState(new HotDayState(this));
                 break;
             case "FlowerEvent":
                 SetState(new FlowerDayState(this));
                 break;
             case "ColdEvent":
                 SetState(new ColdDayState(this));
                 break;
         }
     }
     
}
//Classes Dos Meses
[Serializable]
public class MonthStats
{
    [Header("Name")]
    public string monthName;

    [Header("Events")] 
    public Events[] events;
}
[Serializable]
public class Events
{
    [Header("Name")]
    public string EventName;
    [Header("EventsTrigger")]
    [Range(0,100)]
    public int minTrigger;
    [Range(0,100)]
    public int maxTrigger;
    [Header("Particle")]
    public GameObject dayParticles;
}
