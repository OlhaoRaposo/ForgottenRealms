using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("PlayerStats")]
    public float speed;
    [SerializeField] private CharacterController cc;
    [Header("FlashLight Stats")]
    public GameObject flashLightObject;
    [SerializeField] private Light[] lights;
    [SerializeField] private bool isFlashOn = false;
    [SerializeField] private bool isFlashEnding = false;
    [SerializeField] private bool isFlashOver = false;
    [SerializeField] private float flashLightBattery = 100f;

    private void Awake()
    {
        cc = this.GetComponent<CharacterController>();
        lights = flashLightObject.GetComponentsInChildren<Light>();
    }
    private void Start()
    {
        CheckLight();
        StartCoroutine(DecreaseBattery());
    }
    void Update()
    {
        PlayerMovement();
        UseLight();
    }
    private void UseLight()
    {
        if (Input.GetMouseButtonDown(1)) {
            isFlashOn = true;
            foreach (Light flashLights in lights)
            {
                flashLights.enabled = true;
            }
        }else if (Input.GetMouseButtonUp(1)) {
            isFlashOn = false;
            foreach (Light flashLights in lights)
            {
                flashLights.enabled = false;
            }
        }
    }
    private void CheckLight()
    {
        if (isFlashOn){
            foreach (Light flashLights in lights)
            {
                flashLights.enabled = true;
            }
        }else {
            foreach (Light flashLights in lights)
            {
                flashLights.enabled = false;
            }
        }
    }
    IEnumerator DecreaseBattery()
    {
        yield return new WaitForSeconds(.1f);
        if (isFlashOn && flashLightBattery > 0) {
            flashLightBattery -=Time.deltaTime;
            Debug.Log(flashLightBattery);
        } 
        StartCoroutine(DecreaseBattery());
    }
    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);
    }
}
