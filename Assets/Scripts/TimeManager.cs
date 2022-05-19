using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [HideInInspector]public float GameTime; //Time from game start

    public float CycleTime; // Second time for one cycle

    public event CallBack EventNewCycle;

    private float timer;
    private float cycleCount; //Cycle count from Game Begin
    public void Init()
    {
        GameTime = 0;

        EventNewCycle += OnCyclePassed;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= CycleTime)
        {
            timer = 0;
            if (EventNewCycle != null) EventNewCycle();
        }
    }

    private void OnCyclePassed()
    {
        cycleCount++;
    }

}
