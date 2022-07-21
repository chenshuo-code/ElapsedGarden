using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSable : MonoBehaviour
{
    public static CheckPointSable Instance;

    private Transform fluxGroup;

    private bool isCheckPointActive = false;
    private void Start()
    {
        Instance = this;
        fluxGroup = transform.GetChild(0);
        fluxGroup.gameObject.SetActive(false);
    }
    public void ActiveCheckPoint()
    {
        if (!isCheckPointActive)
        {
            fluxGroup.gameObject.SetActive(true);
            isCheckPointActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isCheckPointActive)
        {
            GameManager.Instance.GuideFlux.OnRecharge();
            CloseCheckPoint();
        }
    }
    private void CloseCheckPoint()
    {
        isCheckPointActive = false;
        fluxGroup.gameObject.SetActive(false);
    }
}
