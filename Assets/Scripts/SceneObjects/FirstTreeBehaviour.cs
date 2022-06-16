using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstTreeBehaviour : MonoBehaviour
{
    private CheckPoint checkPoint;
    public void Init()
    {
        checkPoint = transform.GetComponent<CheckPoint>();
        GameManager.Instance.AddCheckPointActive(checkPoint);
    }
}
