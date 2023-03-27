using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePatrolState : StateFSM
{
   private CreatureMovementFSM creature;
   
   public CreaturePatrolState(
      CreatureMovementFSM creature){
      this.creature = creature;
   }
   Vector3 creatureDir;


   public void Enter()
   {
      creatureDir = new Vector3(Random.Range(0,361),0,Random.Range(0,361));
      Debug.Log("PatrolState");
   }

   public void Update()
   {
      Vector3 avoidOffset;

      //Create Rays
      for (int i = 0; i < creature.rayAmount; i++)
      {
         Quaternion rotation = creature.transform.rotation;
         Quaternion rotationModifier = Quaternion.AngleAxis(i / ((float)creature.rayAmount - 1) * creature.angleDetection * 2 - creature.angleDetection, creature.transform.up);
         Vector3 dir = rotation * rotationModifier * Vector3.forward;
         creature.rays.Add(new Ray(creature.transform.position, dir * creature.rayDistance));
         Debug.DrawRay(creature.transform.position,dir * creature.rayDistance,Color.magenta);
      }
      //Make Creature Detect the Object if gets Closer
      foreach (Ray ray in creature.rays)
      {
         if (Physics.Raycast(ray,out RaycastHit hit,creature.rayDistance,creature.objectMask)) {
            avoidOffset = (hit.point - creature.transform.position).normalized;
            creatureDir -= avoidOffset;
            creatureDir.y = creature.transform.position.y;
         }
      }
      creature.transform.LookAt(creature.transform.position + creatureDir);
      
      Debug.DrawRay(creature.transform.position, creatureDir,Color.red);
      
      creature.transform.position += creature.transform.forward * creature.creatureSpeed * Time.deltaTime;
      Debug.Log("Dir: " + creatureDir);
      //Clear Rays
      creature.rays.Clear();
   }
   public void Exit()
   {
   }
}
