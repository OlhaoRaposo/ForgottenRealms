using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CompassScripts : MonoBehaviour
{
    private Transform playerObj;
    public RawImage compassDirection;
    public float lerpSpeed;

    private void Awake()
    {
        playerObj = this.transform;
    }

    void Update()
    {
        compassDirection.uvRect = new Rect(playerObj.localEulerAngles.y / 360f, 0, 1f, 1f);
    }
}
