using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
   
   [SerializeField]
   private bool playerHasSteped;
   [SerializeField]
   private SphereCollider thisCol;
   [SerializeField]
   private AudioStats audio;

   private void Awake()
   {
      thisCol = gameObject.GetComponent<SphereCollider>();
   }

   private void OnCollisionEnter(Collision col)
   {
      if (col.gameObject.CompareTag("Player")) {
         if (playerHasSteped == false)
         {
            AudioBoard.instance.PlayAudio(audio.audioName);
            Debug.Log("Col:" + col.gameObject.name);
            playerHasSteped = true;
         }
      }
   }

   private void Update()
   {
      if (playerHasSteped)
      {
         thisCol.isTrigger = true;
         InstantiateAudio();
      }
      foreach (AudioSource audioSc in AudioBoard.instance.audios)
      {
         if (audioSc.name == audio.audioName)
         {
            if (audioSc.isPlaying == false)
            {
               playerHasSteped = false;
            }
         }
      }
   }
   void InstantiateAudio()
   {
      if (thisCol.radius < audio.audioRange)
      {
         float lerpTime;
         float sizeSlower = .01f;
         lerpTime = audio.audioSpeed += Mathf.Lerp(.1f,1,sizeSlower += .5f * Time.deltaTime) * Time.deltaTime;
         thisCol.radius = Mathf.Lerp(0.1f,audio.audioRange,lerpTime);
      }else if (thisCol.radius >= audio.audioRange)  {
         thisCol.radius = audio.minRange;
         playerHasSteped = false;
         thisCol.isTrigger = false;
         audio.audioSpeed = .1f;
      }
      
   }
   
}
[Serializable]
public class AudioStats
{
   [Header("AudioName")]
   public string audioName;
   [Header("Audio Range")]
   public float audioRange;
   public float minRange;
   public float audioSpeed;
}