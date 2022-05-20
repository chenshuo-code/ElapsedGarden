using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [HideInInspector]public float GameTime; //Total game time from begin of the game

    public float TimeScale; //Time scale to active a game time
    public float CycleScale; // count of action for one cycle
    
    public event CallBack EventNewCycle;
    public event CallBack EventTimePass;

    [HideInInspector] public float CycleCount; //Cycle count from Game Begin

    private float  counter;
    private float timer;
    public void Init()
    {
        CycleCount = 0;
        counter = 1;
        GameTime = 0;
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>=TimeScale)
        {
            timer = 0;
            GameTime++;
            if (EventTimePass != null) EventTimePass();
        }
    }
    public  void OnCyclePassed()
    {
        if (EventNewCycle != null) EventNewCycle();
        counter++;
        if (counter>=CycleScale)
        {
            counter = 0;
            CycleCount++;
        }
    }

}
