using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Lotus : MonoBehaviour
{
    /// <summary>
    /// Time delay to show lotus, in secound (>1)
    /// </summary>
    public float ShowWaitTime;

    private TriggerLotus triggerLotus;
    private SkinnedMeshRenderer lotusMesh;
    private bool isLotusActive=false;

    private void Start()
    {
        triggerLotus =  transform.GetComponentInChildren<TriggerLotus>();
        lotusMesh = transform.GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish")&&isLotusActive)
        {
            ShowLotus();
        }
        if (other.CompareTag("Player"))
        {
            ActivePlate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish")&&isLotusActive)
        {
            HideLotus();
        }
    }
    private void ShowLotus()
    {
        //triggerLotus.gameObject.SetActive(true);
        lotusMesh.gameObject.SetActive(true);
    }
    private void HideLotus()
    {
        //triggerLotus.gameObject.SetActive(false);
        lotusMesh.gameObject.SetActive(false);
    }
    private void ActivePlate()
    {
        print("我活啦");
        isLotusActive = false;
        Invoke("DeactivatePlate",5f);
    }
    private void DeactivatePlate()
    {
        print("我又死了");
        HideLotus();
        isLotusActive = false;
    }
    public void ActiveLotus()
    {
        isLotusActive = true;
    }
}
