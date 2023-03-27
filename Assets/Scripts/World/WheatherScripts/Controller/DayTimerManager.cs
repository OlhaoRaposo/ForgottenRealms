using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DayTimerManager : MonoBehaviour
{
    public WheatherManagerFSM wheatherManager;
    [Header("Tempo da metade do dia em minutos")]
    public float halfdaytime;
    private float daytime;

    [Header("Time Text")]
    public Text dayStatusText;

    [Header("DayResume")]
    public GameObject preDayObject;
    public Text preDayText;
    private bool preDay;


    [Header("DayTimer")]
    private int day;
    private int colorIndex;
    private float t = 0f;
    private int len;
    private float time = 120;
    private int monthType;
    private void Start()
    {
        daytime = 21600 + halfdaytime * 60;
    }
    private void Update()
    {
        if (daytime >= 86400) {
            daytime = 21600;
        }
        daytime += Time.deltaTime * (43200 / (halfdaytime * 60));
        AttDataText();
    }
    
    void AttDataText()
    {
        float hour;
        hour = ((daytime / 60) / 60);
        if (hour > 23.99f)
        {
            hour = 0;
        }
        dayStatusText.text = $"Hora atual {hour.ToString("0.00")}\nDia {day + 1}";
    }
}


