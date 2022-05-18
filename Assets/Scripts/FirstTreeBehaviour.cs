using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTreeBehaviour : MonoBehaviour
{
    private float totalLifeTime; //Total life time for player

    public void Init()
    {
        totalLifeTime = 100;
    }
    private void OnMouseDown()
    {
        GameManager.Instance.PlayerController.DrawLine(true);
    }
    private void OnMouseEnter()
    {
        GameManager.Instance.PlayerController.DrawLine(false);
    }

    public void GivingTime(float timeGiven)
    {
        totalLifeTime -= timeGiven;
        print(totalLifeTime);
    }
}
