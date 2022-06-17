using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public bool IsActive=true;

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
