using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public bool IsActive=true;

    private void Start()
    {
        if (IsActive)
        {
            this.gameObject.SetActive(true);
        }
        else this.gameObject.SetActive(false);
    }
    public virtual void ActivateObstacle()
    {
        IsActive = true;
        this.gameObject.SetActive(true);
    }
    public virtual void DeactivateObstacle()
    {
        IsActive = false;
        this.gameObject.SetActive(false);
    }
}
