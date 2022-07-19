using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Door : MonoBehaviour
{
    public int ConditionCount = 7;

    private void CheckDeativateDoor()
    {
        if (ConditionCount == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void TorchResolve()
    {
        ConditionCount--;
        CheckDeativateDoor();
    }
    public void CheckPointResolve()
    {
        ConditionCount--;
        CheckDeativateDoor();
    }
}
